namespace SMHIService.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class ForecastContextDesigner : IDesignTimeDbContextFactory<ForecastContext>
{
  //Add-Migration -Name your-migration-name -Context ForecastContext -Project SmhiService -StartupProject SmhiService
  //Update-Database -Context ForecastContext -Project SmhiService -StartupProject SmhiService

  public ForecastContext CreateDbContext(string[] args)
  {
    IConfiguration configuration = new ConfigurationBuilder()
    .AddUserSecrets<ForecastContext>()
    .Build();

    string? connectionString = configuration.GetConnectionString("SmhiDb");
    ServerVersion serverVersion = ServerVersion.AutoDetect(connectionString);
    DbContextOptionsBuilder<ForecastContext> optionsBuilder = new();
    _ = optionsBuilder.UseMySql(connectionString, serverVersion)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors();

    return new ForecastContext(optionsBuilder.Options);
  }
}

