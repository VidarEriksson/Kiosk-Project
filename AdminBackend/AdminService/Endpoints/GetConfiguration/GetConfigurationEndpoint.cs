namespace AdminService.Endpoints.GetConfiguration;

using System.Linq;

using AdminService.Redis;

using FastEndpoints;

public sealed class GetConfigurationEndpoint(ILogger<GetConfigurationEndpoint> logger, IStorageService service)
  : Endpoint<GetConfigurationRequest, GetConfigurationResponse>
{
  public override void Configure()
  {
    Get("/getconfiguration/{serviceName}");
    AllowAnonymous();
  }

  public override async Task HandleAsync(GetConfigurationRequest r, CancellationToken c)
  {
    logger.LogInformation("Running pipe on GetConfigurationEndpoint");

    var values = service.GetValues(r.ServiceName).ToArray();

    await SendOkAsync(new GetConfigurationResponse { ServiceName = r.ServiceName, Configuration = values }, c);
  }
}
