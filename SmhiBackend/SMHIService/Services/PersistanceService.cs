namespace SMHIService.Services;

using System.Linq;

using Microsoft.EntityFrameworkCore;

using SMHIService.Data;
using SMHIService.Models;

public class PersistanceService(ILogger<PersistanceService> logger, ForecastContext context)
  : IPersistanceService
{
  private readonly ILogger<PersistanceService> logger = logger;
  private readonly ForecastContext context = context;

  public async Task AddForecasts(IEnumerable<Forecast> forecasts)
  {
    IEnumerable<KeyValuePair<int, DateTimeOffset>> datesList = [.. context.Forecasts.AsNoTracking().Select(f => KeyValuePair.Create(f.Id, f.ValidTime))];
    Dictionary<int, DateTimeOffset> dates = datesList.ToDictionary(pair => pair.Key, pair => pair.Value);

    foreach (Forecast forecast in forecasts)
    {
      if (!dates.ContainsValue(forecast.ValidTime))
      {
        logger.LogDebug("Adding forecast for {time}", forecast.ValidTime);
        _ = await context.AddAsync(forecast);
      }
      else
      {
        logger.LogDebug("Updating forecast for {time}", forecast.ValidTime);
        forecast.Id = dates.Where(f => f.Value == forecast.ValidTime).FirstOrDefault().Key;
        _ = context.Update(forecast);
      }

    }

    _ = await context.SaveChangesAsync();
  }

  public async Task AddObservations(IEnumerable<Observation> observations)
  {
    logger.LogDebug("Adding observations");
    
    DateTimeOffset[] dates = context.Observations.AsNoTracking().Select(o => o.MeasuredDate).ToArray();

    await context.AddRangeAsync(observations.Where(o => !dates.Contains(o.MeasuredDate)));

    _ = await context.SaveChangesAsync();
  }
}
