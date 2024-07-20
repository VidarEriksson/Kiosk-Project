namespace AdminService.Endpoints.SetValue;

using FastEndpoints;

public sealed class SetValueMapper
  : ResponseMapper<SetValueResponse, KeyValuePair<string, string>>
{
  public override SetValueResponse FromEntity(KeyValuePair<string, string> e) 
    => new() { Key = e.Key, Value=e.Value};
}