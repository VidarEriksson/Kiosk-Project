
using CalendarService;
using CalendarService.Api;
using CalendarService.Data;
using CalendarService.Services;
using CalenderService.Api;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Serilog;

using System.Net;


var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((context, services, configuration) => configuration
  .ReadFrom.Configuration(context.Configuration)
  .ReadFrom.Services(services)
  .Enrich.FromLogContext());

if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
{
    builder.WebHost.UseKestrel(options =>
    {
        options.Listen(IPAddress.Any, 80);
       
    });
}

var connectionString = builder.Configuration.GetConnectionString("CalendarDB") ??
            throw new ArgumentException("Connectionstring missing, paste in user secret");

// Add services to the container
builder.Services.AddDbContext<CalendarContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService<GoogleCalendarBackgroundService>();
ApplicationInfo appInfo = new(typeof(Program));
builder.Services.AddSingleton(appInfo);
builder.Services.AddHealthChecks().AddCheck("dummy", () =>
    {
        return HealthCheckResult.Healthy("application is running");
});
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

    // Automatisk databasinitialisering och migrering vid start
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<CalendarContext>();
        await dbContext.Database.MigrateAsync();


    }
    app.MapHealthChecks("/healthz");
    // Map API endpoints
    app.MapApiEndpoints();
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }



    app.Run();


