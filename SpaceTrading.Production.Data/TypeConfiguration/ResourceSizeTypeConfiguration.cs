using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpaceTrading.Production.Data.Models;

namespace SpaceTrading.Production.Data.TypeConfiguration
{
    public class ResourceSizeTypeConfiguration : IEntityTypeConfiguration<ResourceSize>
    {
        public void Configure(EntityTypeBuilder<ResourceSize> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(e => e.Resources)
                .WithOne(e => e.ResourceSize)
                .HasForeignKey(e => e.ResourceSizeId)
                .IsRequired();
        }
    }
}