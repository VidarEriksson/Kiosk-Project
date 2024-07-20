namespace SMHIService.Models;
public class Forecast
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
  public double Symbol { get; set; } // Weather symbol
  public double Precipitation { get; set; } // Precipitation type
}

public class Observation
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
