namespace SMHIService.Models;

public static class WeatherTypes
{
  public static readonly string[] PrecipitationTypes =
  {
    "", //"Ingen nederbörd",//"No precipitation",
    "Snö",//"Snow",
    "Snöblandat regn",//"Snow and rain",
    "Regn",//"Rain",
    "Duggregn", //"Drizzle",
    "Underkylt regn", //"Freezing rain",
    "Underkylt duggregn", //"Freezing drizzle"
  };
  public static readonly string[] WeatherSymbols =
  {
    "Clear sky",
    "Nearly clear sky",
    "Variable cloudiness",
    "Halfclear sky",
    "Cloudy sky",
    "Overcast",
    "Fog",
    "Light rain showers",
    "Moderate rain showers",
    "Heavy rain showers",
    "Thunderstorm",
    "Light sleet showers",
    "Moderate sleet showers",
    "Heavy sleet showers",
    "Light snow showers",
    "Moderate snow showers",
    "Heavy snow showers",
    "Light rain",
    "Moderate rain",
    "Heavy rain",
    "Thunder",
    "Light sleet",
    "Moderate sleet",
    "Heavy sleet",
    "Light snowfall",
    "Moderate snowfall",
    "Heavy snowfall"
  };
}
