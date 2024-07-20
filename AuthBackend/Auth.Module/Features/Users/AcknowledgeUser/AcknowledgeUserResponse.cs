﻿namespace Auth.Module.Features.Users.AcknowledgeUser;
public sealed class AcknowledgeUserResponse
{
  public Guid Id { get; set; }
  public required string Email { get; set; }
  public string? PhoneNumber { get; internal set; }
  public IEnumerable<string> Roles { get; set; } = Enumerable.Empty<string>();
}
