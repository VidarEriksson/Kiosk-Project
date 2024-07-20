namespace AdminService.Endpoints.GetServices;

using FastEndpoints;

public sealed class GetServicesMapper
  : ResponseMapper<GetServicesResponse, IEnumerable<string>>
{
  public override GetServicesResponse FromEntity(IEnumerable<string> e) 
    => new(e);
}