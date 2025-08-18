using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrackingFolder.API.Models;

namespace goldExpertApi_minimal.Configurations
{
    /// <summary>
    /// 
    /// </summary>
    public class GoldExpertMachineConfiguration : IEntityTypeConfiguration<GoldExpertMachine>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<GoldExpertMachine> builder)
        {
            // Configure the table name
            builder.ToTable("GoldExpertMachines");

            // Configure the primary key
            builder.HasKey(gem => gem.Id);

            // Configure properties
            builder.Property(gem => gem.Name).IsRequired().HasMaxLength(100);
            builder.Property(gem => gem.FolderPath).HasMaxLength(500);
            builder.Property(gem => gem.SerialNumber).HasMaxLength(50);
            builder.Property(gem => gem.Model).HasMaxLength(50);
            builder.Property(gem => gem.Version).HasMaxLength(50);

            // Seed initial data
            builder.HasData(
                new GoldExpertMachine
                {
                    Id = Guid.NewGuid(),
                    Name = "Gold Expert Machine 1",
                    FolderPath = "/path/to/machine1",
                    SerialNumber = "SN123456",
                    Model = "ModelX",
                    Version = "1.0",
                    IsDeleted = false,
                    ModifiedBy = "system",
                    ModifiedOn = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
                },
                new GoldExpertMachine
                {
                    Id = Guid.NewGuid(),
                    Name = "Gold Expert Machine 2",
                    FolderPath = "/path/to/machine2",
                    SerialNumber = "SN654321",
                    Model = "ModelY",
                    Version = "1.0",
                    IsDeleted = false,
                    ModifiedBy = "system",
                    ModifiedOn = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
                }
            );
        }
    }
}
