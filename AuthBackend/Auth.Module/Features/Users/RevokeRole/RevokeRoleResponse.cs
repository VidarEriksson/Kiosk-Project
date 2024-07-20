namespace Auth.Module.Features.Users.RevokeRole;

using Auth.Module.Model;

public sealed class RevokeRoleResponse
{
  public required string Email { get; set; }
  public required IEnumerable<string> Roles { get; set; }
}
