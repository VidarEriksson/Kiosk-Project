namespace AdminService.Endpoints.SetConfiguration;

using Microsoft.AspNetCore.Mvc;

internal sealed class SetConfigurationRequest
{
  public required string ServiceName { get; set; }
  public required IEnumerable<KeyValuePair<string, string>> Configuration { get; set; }
}
