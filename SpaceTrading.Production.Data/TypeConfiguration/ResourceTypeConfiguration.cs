using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpaceTrading.Production.Data.Models;

namespace SpaceTrading.Production.Data.TypeConfiguration
{
    public class ResourceTypeConfiguration : IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne<ResourceSize>()
                .WithMany(x => x.Resources)
                .HasForeignKey(x => x.ResourceSizeId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            
            builder.HasOne<ResourceCategory>()
                .WithMany(x => x.Resources)
                .HasForeignKey(x => x.ResourceCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}