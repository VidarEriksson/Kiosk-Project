namespace Auth.Module.Features.Users.ChangePassword;

internal sealed class ChangePasswordRequest
{
  public required string OldPassword { get; internal set; }
  public required string NewPassword { get; internal set; }
  public required string ConfirmPassword { get; internal set; }
}
