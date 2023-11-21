namespace SpaceTrading.Production.General.Resources
{
    public class ResourceQuantity
    {
        public Resource Resource { get; set; }
        public int Quantity { get; set; }

        public int Volume => (int)Resource.Size * Quantity;

        public bool HasAmount(ResourceQuantity resourceQuantity)
        {
            return Resource == resourceQuantity.Resource && Quantity >= resourceQuantity.Quantity;
        }
    }
}