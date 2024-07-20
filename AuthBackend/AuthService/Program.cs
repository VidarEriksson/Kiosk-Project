using System.Net;
using System.Reflection;

using Auth.Module;

using AuthService;

using FastEndpoints;
using FastEndpoints.Swagger;

using Microsoft.Extensions.Diagnostics.HealthChecks;

using Serilog;

var builder = WebApplication.CreateBuilder(args);
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

builder.Services.AddHealthChecks().AddCheck("Dummy",() => 
{
  return HealthCheckResult.Healthy("Application is running");
});

builder.Services
  .AddAuthModule(builder.Configuration)
  .AddFastEndpoints(o => o.Assemblies =
    [
      Assembly.GetAssembly(typeof(AuthModuleExtension))!
    ])
  .SwaggerDocument(o =>
  {
    o.MaxEndpointVersion = 1;
    o.DocumentSettings = s =>
    {
      s.DocumentName = "Release 1.0";
      s.Title = "AuthService";
      s.Version = appInfo.SemanticVersion;
      s.Description = appInfo.Description;
    };
  })
  .AddCors(options =>
  {
    options.AddPolicy("CorsPolicy", builder => builder
      .AllowAnyOrigin()
      .AllowAnyMethod()
      .AllowAnyHeader());
  });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
}
//app.UseSwagger();
//app.UseSwaggerUI();

app.UpdateIdentityDb()
  //.UseHttpsRedirection()
  .UseCors("CorsPolicy")
  .UseApiAuth()
  .UseAuthentication()
  .UseAuthorization()
  .UseFastEndpoints()
  .UseSwaggerGen();

app.MapHealthChecks("/healthz");

app.Run();
