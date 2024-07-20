namespace BarometerService;

public interface IDataService
{
  Task<IEnumerable<Measure>> GetMeasures();
}
