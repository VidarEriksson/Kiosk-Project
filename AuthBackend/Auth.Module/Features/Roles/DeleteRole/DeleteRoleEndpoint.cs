namespace Auth.Module.Features.Roles.DeleteRole;
using System.Threading.Tasks;

using Auth.Module.Model;

using FastEndpoints;

//TODO Finish DeleteRoleSummary and DeleteRoleEndpoint (Configure)
public sealed class DeleteRoleEndpoint(IRoleService service)
  : EndpointWithoutRequest<DeleteRoleResponse, DeleteRoleMapper>
{
  public override void Configure()
  {
    Delete("/auth/roles/delete/{name}");
    //AllowAnonymous();
    //Policies("SuperAdminPolicy");
    Policies("AdminPolicy");
    //Policies("UserPolicy");
  }

  public override async Task HandleAsync(CancellationToken c)
  {
    string name = Route<string>("name")!;

    AuthRole? role = await service.DeleteRole(name);
    
    if (role is not null)
    {
      await SendAsync(Map.FromEntity(role), 200, c);
    }
    else
    {
      await SendNotFoundAsync(c);
    }
  }
}
