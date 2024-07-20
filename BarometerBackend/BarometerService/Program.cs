using BarometerService;

using Microsoft.AspNetCore.Mvc;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IDataService, DataService>((sp) => new DataService(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.UseCors();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/barometer", async ([FromServices] IDataService service) =>
{
  IEnumerable<Measure> data = await service.GetMeasures();
  return data;
})
.WithName("Barometer")
.WithOpenApi();

app.Run();
