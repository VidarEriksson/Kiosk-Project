namespace Auth.Module.Features.Users.ResetPassword;

public sealed class ResetPasswordRequest
{
  public required string Email { get; set; }
  public required string Password { get; set; }
  public required string ConfirmPassword { get; set; }
}
