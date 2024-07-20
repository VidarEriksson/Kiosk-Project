namespace SMHIService.Contracts;

public class WeatherForecast
{
  public int Id { get; set; }
  public DateTimeOffset ValidTime { get; set; }
  public double Longitude { get; set; }
  public double Latitude { get; set; }
  public double Temperature { get; set; }
  public double Pressure { get; set; }
  public double Humidity { get; set; }
  public double WindSpeed { get; set; }
  public double WindDirection { get; set; }
  public double CloudCover { get; set; }
  public double SymbolId { get; set; }
  public string? Symbol { get; set; } // Weather symbol path
  public double PrecipitationId { get; set; }
  public string? Precipitation { get; set; } // Precipitation type
}
public class WeatherObservation
{
  public int Id { get; set; }
  public required string StationKey { get; set; }
  public required string StationName { get; set; }
  public double Longitude { get; set; }
  public double Latitude { get; set; }
  public DateTimeOffset Updated { get; set; }
  public DateTimeOffset MeasuredDate { get; set; }
  public required string MeasuredValue { get; set; }
}