namespace Auth.Module.Features.Users.RegisterUser;

public sealed class RegisterUserRequest
{
  public required string Firstname { get; init; }
  public required string Lastname { get; init; }
  public required string Email { get; init; }
  public required string Phonenumber { get; init; }
  public required string Password { get; init; }
  public required string ConfirmPassword { get; init; }
}
