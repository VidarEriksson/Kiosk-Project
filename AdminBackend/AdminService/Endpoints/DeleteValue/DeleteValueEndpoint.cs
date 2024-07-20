namespace AdminService.Endpoints.DeleteValue;

using System.Linq;

using AdminService.Redis;

using FastEndpoints;

public sealed class DeleteValueEndpoint(ILogger<DeleteValueEndpoint> logger, IStorageService service)
  : Endpoint<DeleteValueRequest, DeleteValueResponse, DeleteValueMapper>
{
  public override void Configure()
  {
    Delete("/deletevalue/{serviceName}/{key}");
    AllowAnonymous();
  }

  public override async Task HandleAsync(DeleteValueRequest r, CancellationToken c)
  {
    logger.LogInformation("Running pipe on DeleteValueEndpoint");

    var values = service.DeleteValue(r.ServiceName, r.Key);

    await SendOkAsync(Map.FromEntity(values), c);
  }
}
