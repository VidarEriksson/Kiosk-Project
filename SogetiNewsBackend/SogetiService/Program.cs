using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Refit;
using SogetiNewsConsoleTest;
using SogetiService.Api;
using SogetiService.Data;
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

// Add services to the container
builder.Services.AddDbContext<SogetiNewsDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService<Worker>();
builder.Services.AddRefitClient<ISogetiNewsInterface>()
    .ConfigureHttpClient(c =>
    {
        c.BaseAddress = new Uri("https://www.adress");
    });
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
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<SogetiNewsDbContext>();
    await dbContext.Database.MigrateAsync();


}
app.MapHealthChecks("/healthz");




app.UseSwagger();
app.UseSwaggerUI();


app.MapApiEndpoints();



app.Run();

