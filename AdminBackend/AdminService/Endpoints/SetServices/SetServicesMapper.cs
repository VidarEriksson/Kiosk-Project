namespace AdminService.Endpoints.SetServices;
using FastEndpoints;

sealed class SetServicesMapper
  : ResponseMapper<SetServicesResponse, IEnumerable<string>>
{
  public override SetServicesResponse FromEntity(IEnumerable<string> e)
    => new(e);
}