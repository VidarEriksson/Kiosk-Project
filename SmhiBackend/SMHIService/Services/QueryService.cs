namespace SMHIService.Services;

using Microsoft.EntityFrameworkCore;

using SMHIService.Data;
using SMHIService.Models;

public class QueryService(ILogger<QueryService> logger, ForecastContext context)
  : IQueryService
{
  public Task<IEnumerable<Forecast>> GetForecasts(int days)
  {
    logger.LogDebug("Getting forecasts for {days} days", days);
    return Task.FromResult(
      context.Forecasts
      .AsNoTracking()
      .Where(f => f.ValidTime > DateTimeOffset.UtcNow && f.ValidTime < DateTimeOffset.UtcNow.AddDays(days))
      .OrderBy(f => f.ValidTime)
      .AsEnumerable());
  }

  public Task<IEnumerable<Observation>> GetObservations(int days)
  {
    logger.LogDebug("Getting observations for {days} days", Math.Abs(days));
    return Task.FromResult(
      context.Observations
      .AsNoTracking()
      .Where(o => o.MeasuredDate > DateTimeOffset.UtcNow.AddDays(-1))
      .OrderBy(f => f.MeasuredDate)
      .AsEnumerable());
  }
}
