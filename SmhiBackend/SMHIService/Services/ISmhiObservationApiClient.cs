namespace SMHIService.Services;

using Refit;

using SMHIService.Contracts;

public interface ISmhiObservationApiClient
{
  //BaseUrl: https://opendata-download-metobs.smhi.se
  //https://opendata-download-metobs.smhi.se/api/version/1.0/parameter/1/station/107420/period/latest-months/data.json


  [Get("/api.json")]
  Task<SmhiObservationVersionsForApi> GetVersionsForApi();

  [Get("/api/version/{version}/parameter.json")]
  Task<SmhiObservationParametersForVersion> GetParametersForVersion(string version);

  [Get("/api/version/{version}/parameter/{parameter}/station.json")]
  Task<SmhiObservationStationsForParameter> GetStationsForParameter(string version, string parameter);

  [Get("/api/version/{version}/parameter/{parameter}/station/{station}/period.json")]
  Task<SmhiObservationPeriodsForStation> GetPeriodsForStation(string version, string parameter, string station);

  [Get("/api/version/{version}/parameter/{parameter}/station/{station}/period/{period}/data.json")]
  Task<SmhiObservationsForPeriod> GetObservationsForPeriod(string version, string parameter, string station, string period);
}

/*
Returns an array of all existing versions:
GET /api.json
Example: https://opendata-download-metobs.smhi.se/api.json

Returns an array of all parameters (observationtypes) for the selected version:
GET /api/version/{version}.json
Example: https://opendata-download-metobs.smhi.se/api/version/1.0.json

Returns an array of all the stations (locations) that exists for a parameter (observationtype)
GET /api/version/{version}/parameter/{parameter}.json
Exempel: https://opendata-download-metobs.smhi.se/api/version/1.0/parameter/1.json

Returns an array of all the periods for a station (location) that exists for a parameter (observationtype)
GET /api/version/{version}/parameter/{parameter}/station/{station}/period.json
Exempel: https://opendata-download-metobs.smhi.se/api/version/1.0/parameter/1/station/107420/period.json

Returns an array of all the periods for a station (location) that exists for a parameter (observationtype)
GET /api/version/{version}/parameter/{parameter}/station/{station}/period.json
Exempel: https://opendata-download-metobs.smhi.se/api/version/1.0/parameter/1/station/107420/period/latest-hour/data.json


https://opendata.smhi.se/apidocs/metobs/parameter.html
 */