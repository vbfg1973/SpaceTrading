using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpaceTrading.Production.Data.Models;

namespace SpaceTrading.Production.Data.TypeConfiguration
{
    public class ResourceCategoryTypeConfiguration : IEntityTypeConfiguration<ResourceCategory>
    {
        public void Configure(EntityTypeBuilder<ResourceCategory> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(e => e.Resources)
                .WithOne(e => e.ResourceCategory)
                .HasForeignKey(e => e.ResourceCategoryId)
                .IsRequired().OnDelete(DeleteBehavior.ClientSetNull);
            
            builder.HasIndex(x => x.Name)
                .IsUnique();
        }
    }
}