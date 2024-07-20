namespace AdminService.Endpoints.SetValue;

using Microsoft.AspNetCore.Mvc;

public sealed class SetValueRequest
{
  [FromRoute]
  public required string ServiceName { get; set; }
  [FromRoute]
  public required string Key { get; set; }
  public required string Value { get; set; }
}
