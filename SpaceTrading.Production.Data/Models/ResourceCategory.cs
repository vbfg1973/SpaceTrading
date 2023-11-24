using SpaceTrading.Production.Data.Abstract;

namespace SpaceTrading.Production.Data.Models
{
    public class ResourceCategory : IEntityBase
    {
        public ResourceCategory()
        {
            Resources = new List<Resource>();
        }

        public string Name { get; set; }

        public ICollection<Resource> Resources { get; set; }

        public int Id { get; set; }
    }
}