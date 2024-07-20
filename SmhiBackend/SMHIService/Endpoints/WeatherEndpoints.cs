namespace SMHIService.Endpoints;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using SMHIService.Contracts;
using SMHIService.Extensions;
using SMHIService.Models;
using SMHIService.Services;

public static class WeatherEndpoints
{
  public static IEndpointRouteBuilder MapWeatherEndpoints(this IEndpointRouteBuilder builder)
  {
    RouteGroupBuilder group = builder.MapGroup("Weather");

    _ = group.MapGet("/getforecasts", async Task<Results<Ok<IEnumerable<WeatherForecast>>, NotFound>> ([FromServices] IQueryService service, IConfiguration configuration) =>
    {
      EntityMappers.baseUrl = configuration["BaseUrls:SmhiService"];
      IEnumerable<Forecast> forecasts = await service.GetForecasts(7);
      IEnumerable<WeatherForecast>? response = forecasts.Select(f => f.FromEntity());

      return response is not null && response.Any() ?
                TypedResults.Ok(response) :
                TypedResults.NotFound();
    })
      .WithName("GetForecasts")
      .WithOpenApi();

    _ = group.MapGet("/getobservations", async Task<Results<Ok<IEnumerable<WeatherObservation>>, NotFound>> ([FromServices] IQueryService service) =>
    {
      IEnumerable<Observation> observations = await service.GetObservations(-7);
      IEnumerable<WeatherObservation>? response = observations.Select(o => o.FromEntity());
      return response is not null && response.Any() ?
          TypedResults.Ok(response) :
          TypedResults.NotFound();
    })
      .WithName("GetObservations")
      .WithOpenApi();
    ;

    return builder;
  }
}