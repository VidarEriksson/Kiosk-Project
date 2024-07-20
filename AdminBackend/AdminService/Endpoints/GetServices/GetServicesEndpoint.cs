namespace AdminService.Endpoints.GetServices;

using AdminService.Redis;

using FastEndpoints;

public sealed class GetServicesEndpoint(ILogger<GetServicesEndpoint> logger, IStorageService service)
  : EndpointWithoutRequest<GetServicesResponse, GetServicesMapper>
{
  public override void Configure()
  {
    Get("/getservices");
    AllowAnonymous();
  }

  public override async Task HandleAsync(CancellationToken c)
  {
    logger.LogInformation("Running pipe on GetServicesEndpoint");

    var services = service.GetServices();

    await SendOkAsync(Map.FromEntity(services), c);
  }
}
