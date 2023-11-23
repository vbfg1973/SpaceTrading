using MonoGame.Extended.Entities;
using SpaceTrading.Production.Components;
using SpaceTrading.Production.Components.ResourceProduction;
using SpaceTrading.Production.Components.ResourceProduction.Recipes;
using SpaceTrading.Production.Components.ResourceStorage;
using SpaceTrading.Production.Systems.Production;

namespace SpaceTrading.Production;

public static class WorldExtensions
{
    public static WorldBuilder AddProductionSystems(this WorldBuilder worldBuilder)
    {
        worldBuilder.AddSystem(new ProductionSystem());

        return worldBuilder;
    }

    public static bool DecorateEntityWithProductionFromRecipeComponents(this World world, int entityId,
        ProductionRecipe productionRecipe)
    {
        const int productionRuns = 50;

        var entity = world.GetEntity(entityId);

        if (entity.Has<ResourceProductionComponent>() || entity.Has<ProductionFlagComponent>() ||
            entity.Has<ResourceStorageComponent>()) return false;

        entity.Attach(new ProductionFlagComponent());
        entity.Attach(new ResourceProductionComponent(productionRecipe));
        entity.Attach(new ResourceStorageComponent(productionRecipe.SingleRunVolumeRequired * productionRuns));

        return true;
    }
}