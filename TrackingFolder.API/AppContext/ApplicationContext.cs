using Microsoft.EntityFrameworkCore;
using TrackingFolder.API.Models;

namespace TrackingFolder.API.AppContext
{
    public class ApplicationContext(DbContextOptions<ApplicationContext> options) : DbContext(options)
    {
        // Default schema for the database context
        private const string DefaultSchema = "goldexpertapi";

        // DbSet to represent the collection
        public DbSet<GoldExpertMachine> GoldExpertMachines { get; set; }
        public DbSet<GExMeasureResult> GExMeasureResults { get; set; }

        // Constructor to configure the database context
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema(DefaultSchema);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly); // automatically registers all IEntityTypeConfiguration<T>
                                                                                               // implementations—including GoldExpertConfiguration/GExMeasureResultConfiguration
        }
    }
}
