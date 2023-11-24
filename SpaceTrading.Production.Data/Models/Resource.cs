using SpaceTrading.Production.Data.Abstract;

namespace SpaceTrading.Production.Data.Models
{
    public class Resource : IEntityBase
    {
        public string Name { get; set; }

        public float UnitVolume { get; set; }

        public int ResourceSizeId { get; set; }
        public ResourceSize ResourceSize { get; set; }

        public int ResourceCategoryId { get; set; }
        public ResourceCategory ResourceCategory { get; set; }

        public int Id { get; set; }
    }
}