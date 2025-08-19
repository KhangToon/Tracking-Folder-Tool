using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrackingFolder.API.Models;

namespace TrackingFolder.API.Configurations
{
    public class GExMeasureResultConfiguration : IEntityTypeConfiguration<GExMeasureResult>
    {
        public void Configure(EntityTypeBuilder<GExMeasureResult> builder)
        {
            // Configure the table name
            builder.ToTable("GExMeasureResult");

            // Configure the primary key
            builder.HasKey(x => x.Id);

            // Configure properties
            builder.Property(gem => gem.GExMachineId).IsRequired();
        }
    }
}
