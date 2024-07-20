namespace SMHIService.Services;

using System.Collections.Generic;
using System.Threading.Tasks;

using SMHIService.Models;

public interface IQueryService
{
  Task<IEnumerable<Forecast>> GetForecasts(int days);
  Task<IEnumerable<Observation>> GetObservations(int days);
}