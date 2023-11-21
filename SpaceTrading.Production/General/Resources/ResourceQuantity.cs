namespace SpaceTrading.Production.General.Resources
{
    public class ResourceQuantity
    {
        public Resource Resource { get; init; } = null!;
        public int Quantity { get; set; }

        public int Volume => (int)Resource.Size * Quantity;

        public bool HasAmount(ResourceQuantity resourceQuantity)
        {
            return Resource == resourceQuantity.Resource && Quantity >= resourceQuantity.Quantity;
        }
    }
}