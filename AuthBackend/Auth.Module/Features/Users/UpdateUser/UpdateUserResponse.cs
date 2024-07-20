namespace Auth.Module.Features.Users.UpdateUser;
using System;

public sealed class UpdateUserResponse
{
  public required string Email { get; set; }
  public required string Firstname { get; set; }
  public required string Lastname { get; set; }
  public required string PhoneNumber { get; set; }
}
