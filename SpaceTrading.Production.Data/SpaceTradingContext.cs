using Microsoft.EntityFrameworkCore;
using SpaceTrading.Production.Data.Models;

namespace SpaceTrading.Production.Data
{
    public class SpaceTradingContext : DbContext
    {
        private readonly string _connectionString;

        public SpaceTradingContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SpaceTradingContext(DbContextOptions<SpaceTradingContext> options) : base(options)
        {
        }

        public SpaceTradingContext()
        {
        }

        public virtual DbSet<Resource> Resources { get; set; } = null!;
        public virtual DbSet<ResourceCategory> ResourcesCategories { get; set; } = null!;
        public virtual DbSet<ResourceSize> ResourceSizes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Latin1_General_CI_AS");

            modelBuilder.ApplyConfigurationsFromAssembly(ProductionDataAssemblyReference.Assembly);

            // OnModelCreatingPartial(modelBuilder);
        }
    }
}