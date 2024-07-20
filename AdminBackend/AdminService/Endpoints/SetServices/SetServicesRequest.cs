namespace AdminService.Endpoints.SetServices;

internal sealed class SetServicesRequest
{
  public required IEnumerable<string> Services { get; set; }
}
