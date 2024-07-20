namespace AdminService.Endpoints.GetConfiguration;

using FastEndpoints;

public sealed class GetConfigurationSummary : Summary<GetConfigurationEndpoint>
{
  public GetConfigurationSummary()
  {
    //TODO: Add Swagger properties here
    Summary = "Summary text goes here...";
    Description = "Description text goes here...";
  }
}
