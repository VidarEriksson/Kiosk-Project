namespace AdminService.Endpoints.DeleteValue;

using FastEndpoints;

public sealed class DeleteValueMapper
  : ResponseMapper<DeleteValueResponse, KeyValuePair<string, string>>
{
  public override DeleteValueResponse FromEntity(KeyValuePair<string, string> e) 
    => new() { Key = e.Key, Value=e.Value};
}