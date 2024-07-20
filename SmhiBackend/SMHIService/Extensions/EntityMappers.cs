namespace SMHIService.Extensions;

using System.Reflection.Metadata.Ecma335;

using SMHIService.Contracts;
using SMHIService.Models;

public static class EntityMappers
{
  public static string baseUrl = string.Empty;
  public static IEnumerable<Forecast> ToEntity(this SmhiForecast forecast)
  { 
    var result = new List<Forecast>();
    foreach (var ts in forecast.TimeSeries!)
    {
      result.Add(new Forecast
      {
        Longitude = forecast.Geometry!.Coordinates![0][0],
        Latitude = forecast.Geometry!.Coordinates![0][1],
        ValidTime = ts.ValidTime,
        Temperature = ts.Parameters!.First(p => p.Name == "t").Values!.First(),
        Pressure = ts.Parameters!.First(p => p.Name == "msl").Values!.First(),
        WindSpeed = ts.Parameters!.First(p => p.Name == "ws").Values!.First(),
        WindDirection = ts.Parameters!.First(p => p.Name == "wd").Values!.First(),
        Humidity = ts.Parameters!.First(p => p.Name == "r").Values!.First(),
        CloudCover = ts.Parameters!.First(p => p.Name == "tcc_mean").Values!.First(),
        Symbol = ts.Parameters!.First(p => p.Name == "Wsymb2").Values!.First(),
        Precipitation = ts.Parameters!.First(p => p.Name == "pcat").Values!.First(),
      });
    }

    return result;
  }

  public static IEnumerable<Observation> ToEntity(this SmhiObservationsForPeriod observation)
  {
    var result = new List<Observation>();

    foreach (var value in observation.Values!)
    {
      result.Add(new Observation
      {
        StationKey=observation.Station!.Key!,
        StationName=observation.Station!.Name!,
        Longitude = observation.Positions!.OrderBy(p=>p.From).Last().Longitude,
        Latitude = observation.Positions!.OrderBy(p => p.From).Last().Latitude,
        Updated=observation.Updated,
        MeasuredDate=value.Date,
        MeasuredValue=value.Measured!,
      });
    }
    return result;
  }

  public static WeatherForecast FromEntity(this Forecast forecast) =>
  new WeatherForecast 
  { 
    Id = forecast.Id,
    Longitude = forecast.Longitude,
    Latitude = forecast.Latitude,
    ValidTime = forecast.ValidTime,
    Temperature = forecast.Temperature,
    Pressure = forecast.Pressure,
    Humidity = forecast.Humidity,
    WindDirection = forecast.WindDirection,
    WindSpeed = forecast.WindSpeed,
    CloudCover = forecast.CloudCover,
    SymbolId = forecast.Symbol,
    //Symbol = WeatherTypes.WeatherSymbols[(int)forecast.Symbol],
    Symbol=$"{baseUrl}/images/{(int)forecast.Symbol}.png",
    PrecipitationId = forecast.Precipitation,
    Precipitation = WeatherTypes.PrecipitationTypes[(int)forecast.Precipitation],
  };

  public static WeatherObservation FromEntity(this Observation observation) =>
  new WeatherObservation 
  { 
    Id = observation.Id,
    StationKey=observation.StationKey,
    StationName=observation.StationName,
    Longitude = observation.Longitude,
    Latitude = observation.Latitude,
    Updated=observation.Updated,
    MeasuredDate=observation.MeasuredDate,
    MeasuredValue=observation.MeasuredValue,
  };
}

