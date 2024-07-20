namespace AdminService.Endpoints.SetServices;
using FastEndpoints;

sealed class SetServicesSummary : Summary<SetServicesEndpoint>
{
  public SetServicesSummary()
  {
    //TODO: Add Swagger properties here
    Summary = "Summary text goes here...";
    Description = "Description text goes here...";
  }
}
