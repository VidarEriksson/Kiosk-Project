namespace Auth.Module.Features.Users.AcknowledgePassword;
using System.Threading.Tasks;

using Auth.Module.Model;

using FastEndpoints;

//TODO Finish AcknowledgePasswordSummary and AcknowledgePasswordEndpoint (Configure)
public sealed class AcknowledgePasswordEndpoint(IUserService service)
  : EndpointWithoutRequest<AcknowledgePasswordResponse, AcknowledgePasswordMapper>
{
  public override void Configure()
  {
    //TODO Change to use ?token={token}, right now it's using /{token}
    Get("/auth/password/acknowledge/{token}");
    AllowAnonymous();
    //Policies("SuperAdminPolicy");
    //Policies("AdminPolicy");
    //Policies("UserPolicy");
  }

  public override async Task HandleAsync(CancellationToken c)
  {
    var token = Route<string>("token");

    AuthUser? user = await service.AcknowledgePassword(token);

    if (user is not null)
    {
      await SendAsync(Map.FromEntity(user), 200, c);
    }
    else
    {
      await SendNotFoundAsync(c);
    }
  }
}
