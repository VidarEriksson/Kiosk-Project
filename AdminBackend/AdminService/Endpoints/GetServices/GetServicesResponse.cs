namespace AdminService.Endpoints.GetServices;

public sealed class GetServicesResponse(IEnumerable<string> e) 
  : List<string>(e)
{
}
