namespace Auth.Module.Features.Users.UpdateUser;
using System.Security.Claims;

using FastEndpoints;

public sealed class UpdateUserRequest
{
  [FromClaim(claimType: ClaimTypes.Email)]
  public string? Emailaddress { get; set; }
  
  public required string Firstname { get; set; }
  public required string Lastname { get; set; }
  public required string PhoneNumber { get; set; }
}
