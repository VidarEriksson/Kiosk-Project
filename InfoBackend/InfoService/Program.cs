using InfoService.Api;
using InfoService.Data;
using InfoService.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net;

var builder = WebApplication.CreateBuilder(args);
if (Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") == "Production")
{
    builder.WebHost.UseKestrel(options =>
    {
        options.Listen(IPAddress.Any, 80);

    });
}

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
            throw new ArgumentException("Connectionstring missing, paste in user secret");


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks().AddCheck("dummy", () =>
{
    return HealthCheckResult.Healthy("application is running");
});
builder.Services.AddScoped<ITextFileContentService, TextFileContentService>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        _ = policy.AllowAnyOrigin()
          .AllowAnyHeader()
          .AllowAnyMethod();
    });
});

var app = builder.Build();
app.UseCors();
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await dbContext.Database.MigrateAsync();
}
app.MapHealthChecks("/healthz");
//app.UseAntiforgery();
app.MapApiEndpoints();
app.UseSwagger();
app.UseSwaggerUI();

app.Run();

