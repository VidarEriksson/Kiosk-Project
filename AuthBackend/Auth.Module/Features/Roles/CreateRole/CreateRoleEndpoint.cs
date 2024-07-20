namespace Auth.Module.Features.Roles.CreateRole;
using System.Threading.Tasks;

using Auth.Module.Model;

using FastEndpoints;

//TODO Finish CreateRolesSummary and CreateRolesEndpoint (Configure)
public sealed class CreateRoleEndpoint(IRoleService service)
  : EndpointWithoutRequest<CreateRoleResponse, CreateRoleMapper>
{
  public override void Configure()
  {
    Post("/auth/roles/create/{name}");
    //AllowAnonymous();
    //Policies("SuperAdminPolicy");
    Policies("AdminPolicy");
    //Policies("UserPolicy");
  }

  public override async Task HandleAsync(CancellationToken c)
  {
    string name = Route<string>("name")!;

    AuthRole? role = await service.CreateRole(name);
    
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
