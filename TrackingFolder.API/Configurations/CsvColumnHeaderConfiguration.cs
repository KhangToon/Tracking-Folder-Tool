using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrackingFolder.API.Models;

namespace TrackingFolder.API.Configurations
{
    public class CsvColumnHeaderConfiguration : IEntityTypeConfiguration<CsvColumnHeader>
    {
        public void Configure(EntityTypeBuilder<CsvColumnHeader> builder)
        {
            // Configure the table name
            builder.ToTable("CsvColumnHeader");

            // Configure the primary key
            builder.HasKey(x => x.Id);

            // Configure properties
            builder.Property(gem => gem.HeaderName).HasMaxLength(200).IsRequired();

            // Seed initial data
            builder.HasData(
                new CsvColumnHeader { Id = Guid.NewGuid(), HeaderName = "Date" },
                new CsvColumnHeader { Id = Guid.NewGuid(), HeaderName = "Time" },
                new CsvColumnHeader { Id = Guid.NewGuid(), HeaderName = "Reading" },
                new CsvColumnHeader { Id = Guid.NewGuid(), HeaderName = "Elapsed Time Total" },
                new CsvColumnHeader { Id = Guid.NewGuid(), HeaderName = "Ni" },
                new CsvColumnHeader { Id = Guid.NewGuid(), HeaderName = "Ni +/-" },
                new CsvColumnHeader { Id = Guid.NewGuid(), HeaderName = "Cu" },
                new CsvColumnHeader { Id = Guid.NewGuid(), HeaderName = "Cu +/-" },
                new CsvColumnHeader { Id = Guid.NewGuid(), HeaderName = "Zn" },
                new CsvColumnHeader { Id = Guid.NewGuid(), HeaderName = "Zn +/-" },
                new CsvColumnHeader { Id = Guid.NewGuid(), HeaderName = "Ag" },
                new CsvColumnHeader { Id = Guid.NewGuid(), HeaderName = "Ag +/-" },
                new CsvColumnHeader { Id = Guid.NewGuid(), HeaderName = "Au" },
                new CsvColumnHeader { Id = Guid.NewGuid(), HeaderName = "Au +/-" },
                new CsvColumnHeader { Id = Guid.NewGuid(), HeaderName = "Pass/Fail" },
                new CsvColumnHeader { Id = Guid.NewGuid(), HeaderName = "Live Time Total" }
            );
        }
    }
}
