namespace SMHIService.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SMHIService.Models;

public class ForecastConfiguration : IEntityTypeConfiguration<Forecast>
{
  public void Configure(EntityTypeBuilder<Forecast> builder)
  {
    _ = builder.HasKey(f => f.Id);
    _ = builder.HasIndex(f => f.ValidTime);
  }
}
