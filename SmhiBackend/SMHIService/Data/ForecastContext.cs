namespace SMHIService.Data;

using Microsoft.EntityFrameworkCore;

using SMHIService.Models;

public class ForecastContext(DbContextOptions<ForecastContext> options)
  : DbContext(options)
{
  public DbSet<Forecast> Forecasts => Set<Forecast>();
  public DbSet<Observation> Observations => Set<Observation>();

  protected override void OnModelCreating(ModelBuilder builder)
    => builder.ApplyConfigurationsFromAssembly(typeof(ForecastContext).Assembly);

  public void EnsureDbExists()
  {
    if (Database.GetPendingMigrations().Any())
    {
      Database.Migrate();
    }
  }
}
