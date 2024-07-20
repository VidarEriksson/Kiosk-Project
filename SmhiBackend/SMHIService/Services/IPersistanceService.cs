namespace SMHIService.Services;

using System.Collections.Generic;
using System.Threading.Tasks;

using SMHIService.Models;

public interface IPersistanceService
{
  Task AddForecasts(IEnumerable<Forecast> forecasts);
  Task AddObservations(IEnumerable<Observation> observations);
}
