namespace Auth.Module.Features.Roles.GetRoles;

using Auth.Module.Model;

public sealed class GetRolesResponse
{
    public IEnumerable<string>? Roles { get; set; }
}
