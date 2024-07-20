namespace Auth.Module.Features.Users.LoginUser;

public sealed class LoginUserResponse
{
  //Guid id, string? userName, IEnumerable<string> roles, string jwt, string refresh
  public required Guid Id { get; set; }
  public required string Firstname { get; set; }
  public required string Lastname { get; set; }
  public required string? Email { get; set; }
  public required IEnumerable<string> Roles { get; set; }
  public required string JwtToken { get; set; }
  public required string RefreshToken { get; set; }
}
