namespace AdminService.Endpoints.DeleteValue;

using Microsoft.AspNetCore.Mvc;

public sealed class DeleteValueRequest
{
  [FromRoute]
  public required string ServiceName { get; set; }
  [FromRoute]
  public required string Key { get; set; }
}
