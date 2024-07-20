namespace AdminService.Endpoints.SetServices;

sealed class SetServicesResponse(IEnumerable<string> services) 
  : List<string>(services)
{
}
