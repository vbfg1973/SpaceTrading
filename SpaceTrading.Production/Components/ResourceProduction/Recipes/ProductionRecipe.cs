using SpaceTrading.Production.General.Resources;

namespace SpaceTrading.Production.Components.ResourceProduction.Recipes
{
    public record ProductionRecipe(ResourceQuantity ResourceQuantity, ProductionRecipeIngredients Ingredients,
        float TimeTaken)
    {
        public float SingleRunVolumeRequired => ResourceQuantity.Volume + Ingredients.Volume;

        public bool TryGetScaledIngredients(ResourceQuantity resourceQuantity,
            out ProductionRecipeIngredients productionRecipeIngredients)
        {
            var scalar = resourceQuantity.Quantity / ResourceQuantity.Quantity;

            productionRecipeIngredients = new ProductionRecipeIngredients();
            productionRecipeIngredients.AddRange(Ingredients.Select(x => new ResourceQuantity
                { Resource = x.Resource, Quantity = x.Quantity * scalar }));

            return true;
        }
    }
}