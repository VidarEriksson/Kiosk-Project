namespace AdminService.Endpoints.SetValue;

using System.Linq;

using AdminService.Redis;

using FastEndpoints;

public sealed class SetValueEndpoint(ILogger<SetValueEndpoint> logger, IStorageService service)
  : Endpoint<SetValueRequest, SetValueResponse, SetValueMapper>
{
  public override void Configure()
  {
    Post("/SetValue/{serviceName}/{key}");
    AllowAnonymous();
  }

  public override async Task HandleAsync(SetValueRequest r, CancellationToken c)
  {
    logger.LogInformation("Running pipe on SetValueEndpoint");

    var values = service.SetValue(r.ServiceName, r.Key, r.Value);

    await SendOkAsync(Map.FromEntity(values), c);
  }
}
