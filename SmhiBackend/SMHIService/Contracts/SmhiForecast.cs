namespace SMHIService.Contracts;
using System;
using System.Text.Json.Serialization;

public class SmhiForecast
{
  [JsonPropertyName("approvedTime")]
  public DateTime ApprovedTime { get; set; }
  [JsonPropertyName("referenceTime")]
  public DateTime ReferenceTime { get; set; }
  [JsonPropertyName("geometry")]
  public SmhiForecastGeometry? Geometry { get; set; }
  [JsonPropertyName("timeSeries")]
  public SmhiForecastTimeserie[]? TimeSeries { get; set; }
}

public class SmhiForecastGeometry
{
  [JsonPropertyName("type")]
  public string? GeometryType { get; set; }
  [JsonPropertyName("coordinates")]
  public float[][]? Coordinates { get; set; }
}

public class SmhiForecastTimeserie
{
  [JsonPropertyName("validTime")]
  public DateTime ValidTime { get; set; }
  [JsonPropertyName("parameters")]
  public SmhiForecastParameter[]? Parameters { get; set; }
}

public class SmhiForecastParameter
{
  [JsonPropertyName("name")]
  public string? Name { get; set; }
  [JsonPropertyName("levelType")]
  public string? LevelType { get; set; }
  [JsonPropertyName("level")]
  public int Level { get; set; }
  [JsonPropertyName("unit")]
  public string? Unit { get; set; }
  [JsonPropertyName("values")]
  public float[]? Values { get; set; }
}

