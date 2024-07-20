namespace AdminService.Endpoints.GetConfiguration;

using Microsoft.AspNetCore.Mvc;

public sealed class GetConfigurationRequest
{
  [FromRoute]
  public required string ServiceName { get; set; }
}
