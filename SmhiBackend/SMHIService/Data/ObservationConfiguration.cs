namespace SMHIService.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SMHIService.Models;

public class ObservationConfiguration : IEntityTypeConfiguration<Observation>
{
  public void Configure(EntityTypeBuilder<Observation> builder)
  {
    _ = builder.HasKey(f => f.Id);
    _ = builder.HasIndex(f => f.MeasuredDate);
  }
}