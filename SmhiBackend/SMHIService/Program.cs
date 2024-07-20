using System.Net;

using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.FileProviders;

using Serilog;

using SMHIService;
using SMHIService.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((context, services, configuration) => configuration
  .ReadFrom.Configuration(context.Configuration)
  .ReadFrom.Services(services)
  .Enrich.FromLogContext());

if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
{
  _ = builder.WebHost.UseKestrel(options =>
  {
    options.Listen(IPAddress.Any, 80);
  });
}

ApplicationInfo appInfo = new(typeof(Program));
builder.Services.AddSingleton(appInfo);

builder.Services.AddHealthChecks().AddCheck("Dummy", () =>
{
  return HealthCheckResult.Healthy("Application is running");
});

builder.Services
  .AddSmhiIntegration(builder.Configuration)
  .AddPersistance(builder.Configuration.GetConnectionString("SmhiDb")!)
  .AddEndpointsApiExplorer()
  .AddSwaggerGen();
//.AddSecurity(builder.Configuration);

builder.Services.AddCors(options =>
{
  options.AddDefaultPolicy(policy =>
  {
    _ = policy.AllowAnyOrigin()
      .AllowAnyHeader()
      .AllowAnyMethod();
  });
});

WebApplication app = builder.Build();

app.UsePersistance();

app.UseCors();

app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
  FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "images")),
  RequestPath = "/images"
});
//Enable directory browsing
app.UseDirectoryBrowser(new DirectoryBrowserOptions
{
  FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "images")),
  RequestPath = "/images"
});

app.UseSwagger();
app.UseSwaggerUI();

//app.UseSecurity();

app.UseEndpoints();

app.MapHealthChecks("/healthz");

app.Run();
