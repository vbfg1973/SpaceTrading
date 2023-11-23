namespace SpaceTrading.Production.General.Resources
{
    public class ResourceQuantity : IEquatable<ResourceQuantity>
    {
        public Resource Resource { get; init; } = null!;
        public int Quantity { get; set; }

        public int Volume => (int)Resource.Size * Quantity;

        public bool Equals(ResourceQuantity? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Resource.Equals(other.Resource) && Quantity == other.Quantity;
        }

        public bool HasAmount(ResourceQuantity resourceQuantity)
        {
            return Resource == resourceQuantity.Resource && Quantity >= resourceQuantity.Quantity;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ResourceQuantity)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Resource, Quantity);
        }

        public static bool operator ==(ResourceQuantity? left, ResourceQuantity? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ResourceQuantity? left, ResourceQuantity? right)
        {
            return !Equals(left, right);
        }
    }
}