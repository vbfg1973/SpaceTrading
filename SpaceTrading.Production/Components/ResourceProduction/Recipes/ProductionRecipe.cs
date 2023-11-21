using SpaceTrading.Production.General.Resources;

namespace SpaceTrading.Production.Components.ResourceProduction.Recipes
{
    public record ProductionRecipe
    {
        public ResourceQuantity ResourceQuantity { get; set; }
        public ProductionRecipeIngredients Ingredients { get; set; }
        public float TimeTaken { get; set; }
    }
}