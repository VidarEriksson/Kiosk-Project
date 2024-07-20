
namespace SMHIService;

using System.Text.Json;

using Microsoft.Extensions.DependencyInjection;

using SMHIService.Extensions;
using SMHIService.Models;
using SMHIService.Services;

public class Worker(ILogger<Worker> logger, IServiceScopeFactory scopeFactory) 
  : BackgroundService
{
  private readonly ILogger<Worker> logger = logger;
  private ISmhiForecastApiClient? forecasts;
  private ISmhiObservationApiClient? observations;
  private IPersistanceService? persistance;

  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    while (!stoppingToken.IsCancellationRequested)
    {
      using var scope = scopeFactory.CreateAsyncScope();

      forecasts = scope.ServiceProvider
        .GetRequiredService<ISmhiForecastApiClient>();
      observations = scope.ServiceProvider
        .GetRequiredService<ISmhiObservationApiClient>();
      persistance = scope.ServiceProvider
        .GetRequiredService<IPersistanceService>();
      
      int intervalMinutes = scope.ServiceProvider
        .GetRequiredService<IConfiguration>()
        .GetValue<int>("Worker:IntervalMinutes");

      var forecast = await forecasts.GetForecasts("pmp3g", "2", "point", "17.1472", "60.6838");
      var entityForecast = forecast.ToEntity();
      await persistance.AddForecasts(entityForecast);

      //using var file = new StreamWriter("./forecasts.json");
      //await file.WriteAsync(JsonSerializer.Serialize(forecast));
      //await file.FlushAsync();


      var observation = await observations.GetObservationsForPeriod("1.0", "1", "75001", "latest-day");
      var entityObservations = observation.ToEntity();
      await persistance.AddObservations(entityObservations);

      //using var file = new StreamWriter("./observations.json");
      //await file.WriteAsync(JsonSerializer.Serialize(forecast));
      //await file.FlushAsync();

      logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

      await Task.Delay(TimeSpan.FromMinutes(intervalMinutes), stoppingToken);
    }
  }
}
