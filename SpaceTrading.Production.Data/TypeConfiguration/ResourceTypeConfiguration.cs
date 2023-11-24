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
        }
    }
}