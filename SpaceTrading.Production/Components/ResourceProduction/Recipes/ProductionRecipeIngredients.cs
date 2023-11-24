using SpaceTrading.Production.General.Resources;

namespace SpaceTrading.Production.Components.ResourceProduction.Recipes
{
    public class ProductionRecipeIngredients : List<ResourceQuantity>, IEquatable<ProductionRecipeIngredients>
    {
        public float Volume => this.Sum(rq => (int)rq.Resource.Size * rq.Quantity);

        public bool Equals(ProductionRecipeIngredients? other)
        {
            return other != null && OrderedBySize().SequenceEqual(other.OrderedBySize());
        }

        internal IEnumerable<ResourceQuantity> OrderedBySize()
        {
            return this.OrderByDescending(x => x.Quantity).ThenBy(x => x.Resource.Name);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((ProductionRecipeIngredients)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(OrderedBySize().Select(x => x.GetHashCode()));
        }

        public static bool operator ==(ProductionRecipeIngredients? left, ProductionRecipeIngredients? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ProductionRecipeIngredients? left, ProductionRecipeIngredients? right)
        {
            return !Equals(left, right);
        }
    }
}