namespace SMHIService.Services;

using Refit;

using SMHIService.Contracts;

public interface ISmhiForecastApiClient
{
  //BaseUrl: https://opendata-download-metfcst.smhi.se
  //https://opendata-download-metfcst.smhi.se/api/category/pmp3g/version/2/geotype/point/lon/17.160666/lat/60.716082/data.json

  [Get("/api/category/{category}/version/{version}/geotype/{geotype}/lon/{longitude}/lat/{latitude}/data.json")]
  Task<SmhiForecast> GetForecasts(string category, string version, string geotype, string longitude, string latitude);
}
