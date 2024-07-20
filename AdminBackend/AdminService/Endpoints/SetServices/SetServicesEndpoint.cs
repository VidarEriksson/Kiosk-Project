namespace AdminService.Endpoints.SetServices;

using AdminService.Redis;

using FastEndpoints;

using Microsoft.Extensions.Logging;

internal sealed class SetServicesEndpoint(ILogger<SetServicesEndpoint> logger, IStorageService service)
  : Endpoint<SetServicesRequest, SetServicesResponse, SetServicesMapper>
{
  public override void Configure()
  {
    Post("/setservices");
    AllowAnonymous();
  }

  public override async Task HandleAsync(SetServicesRequest r, CancellationToken c)
  {
    logger.LogInformation("Running pipe on SetServicesEndpoint");
    
    service.SetServices(r.Services);

    await SendOkAsync(Map.FromEntity(r.Services), c);
  }
}
