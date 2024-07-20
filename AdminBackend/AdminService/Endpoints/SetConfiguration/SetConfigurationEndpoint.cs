namespace AdminService.Endpoints.SetConfiguration;

using System.Collections.Concurrent;

using AdminService.Redis;

using FastEndpoints;

using Microsoft.Extensions.Logging;

internal sealed class SetConfigurationEndpoint(ILogger<SetConfigurationEndpoint> logger, IStorageService service)
  : Endpoint<SetConfigurationRequest, SetConfigurationResponse>
{
  public override void Configure()
  {
    Post("/setconfiguration");
    AllowAnonymous();
    //AllowFileUploads(dontAutoBindFormData: true);
  }

  public override async Task HandleAsync(SetConfigurationRequest r, CancellationToken c)
  {
    logger.LogInformation("Running pipe on SetConfigurationEndpoint");
    
    ConcurrentDictionary<string,string> nisse = new();

    service.SetValues(r.ServiceName, r.Configuration.ToDictionary<string,string>());

    await SendOkAsync();
  }
}
