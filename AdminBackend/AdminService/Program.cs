using System.Net;
using System.Text.Json.Serialization;
using System.Text.Json;

//using AdminService.Data.Context;

using FastEndpoints;
using AdminService;
using FastEndpoints.Swagger;
using AdminService.Redis;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

//if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
{
  _ = builder.WebHost.UseKestrel(options =>
  {
    options.Listen(IPAddress.Any, 80);
  });
}

ApplicationInfo appInfo = new(typeof(Program));
builder.Services.AddSingleton(appInfo);

builder.Services.ConfigureHttpJsonOptions(options =>
{
  options.SerializerOptions.PropertyNameCaseInsensitive = true;
  options.SerializerOptions.WriteIndented = true;
  options.SerializerOptions.AllowTrailingCommas = true;
  options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
  options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.Configure<RedisConfiguration>(options =>
  builder.Configuration.GetSection(RedisConfiguration.Redis).Bind(options));
builder.Services.AddTransient<IStorageService, StorageService>();

builder.Services
  .AddFastEndpoints()
  .SwaggerDocument();

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
app.AddTestData(["AuthService", "CalendarService", "BarometerService", "InfoService", "SogetiService", "ImageService"]);
app.UseCors();

app.UseFastEndpoints(c =>
{
  c.Serializer.Options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
  c.Serializer.Options.PropertyNameCaseInsensitive = true;
  c.Serializer.Options.Converters.Add(new JsonStringEnumConverter());
})
  .UseSwaggerGen();

app.Run();

