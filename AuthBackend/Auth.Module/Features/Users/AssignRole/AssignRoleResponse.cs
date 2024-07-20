namespace Auth.Module.Features.Users.AssignRole;

using Auth.Module.Model;

public sealed class AssignRoleResponse
{
  public required string Email { get; set; }
  public required IEnumerable<string> Roles { get; set; }
}
