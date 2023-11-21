using MonoGame.Extended.Entities;
using SpaceTrading.Production.Components.ResourceProduction;
using SpaceTrading.Production.Components.ResourceProduction.Recipes;
using SpaceTrading.Production.Components.ResourceStorage;

namespace SpaceTrading.Production
{
    public static class WorldExtensions
    {
        public static void CreateProductionEntityFromRecipe(this World world, ProductionRecipe productionRecipe)
        {
            const int productionRuns = 50;

            var random = new Random();
            var entity = world.CreateEntity();
            // entity.Attach(new Location(random.Next(0, 800), random.Next(0, 640), 3));
            entity.Attach(new ResourceProductionComponent(productionRecipe));
            entity.Attach(new ResourceStorageComponent(
                (productionRecipe.Ingredients.Volume + productionRecipe.ResourceQuantity.Volume) * productionRuns));
        }
    }
}