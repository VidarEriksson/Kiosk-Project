namespace AdminService.Endpoints.GetConfiguration;

public sealed class GetConfigurationResponse
{
  public required string ServiceName { get; set; }
  public required IEnumerable<KeyValuePair<string,string>> Configuration { get; set; }
}
