namespace AdminService.Endpoints.SetConfiguration;

sealed class SetConfigurationResponse
{
  public required string ServiceName { get; set; }
  public required IEnumerable<KeyValuePair<string, string>> Configuration { get; set; }
}
