namespace Auth.Module.Features.Roles.GetRoles;
using System.Threading.Tasks;

using Auth.Module.Model;

using FastEndpoints;

using Microsoft.AspNetCore.Authorization;

//TODO Finish GetRolesSummary and GetRolesEndpoint (Configure)
public sealed class GetRolesEndpoint(IRoleService service)
  : EndpointWithoutRequest<GetRolesResponse, GetRolesMapper>
{
  public override void Configure()
  {
    Get("/auth/roles");
    //AllowAnonymous();
    //Policies("SuperAdminPolicy");
    //Policies("AdminPolicy");
    Policies("UserPolicy");
  }

  public override async Task HandleAsync(CancellationToken c)
  {
    IEnumerable<AuthRole> roles = await service.GetRoles();

    if (roles is not null && roles.Any())
    {
      await SendAsync(Map.FromEntity(roles), 200, c);
    }
    else
    {
      await SendNotFoundAsync(c);
    }
  }
}
