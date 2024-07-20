namespace AdminService.Endpoints.SetConfiguration;
using FastEndpoints;

sealed class SetConfigurationSummary : Summary<SetConfigurationEndpoint>
{
  public SetConfigurationSummary()
  {
    //TODO: Add Swagger properties here
    Summary = "Summary text goes here...";
    Description = "Description text goes here...";
  }
}
