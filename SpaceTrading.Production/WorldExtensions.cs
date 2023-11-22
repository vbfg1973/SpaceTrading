﻿using MonoGame.Extended.Entities;
using SpaceTrading.Production.Components.ResourceProduction;
using SpaceTrading.Production.Components.ResourceProduction.Recipes;
using SpaceTrading.Production.Components.ResourceStorage;
using SpaceTrading.Production.Systems;

namespace SpaceTrading.Production
{
    public static class WorldExtensions
    {
        public static WorldBuilder AddProductionSystems(this WorldBuilder worldBuilder)
        {
            worldBuilder.AddSystem(new ProductionSystem());

            return worldBuilder;
        }
        
        public static int DecorateEntityProductionFromRecipe(this World world, int entityId, ProductionRecipe productionRecipe)
        {
            const int productionRuns = 50;

            var entity = world.GetEntity(entityId);
            
            entity.Attach(new ResourceProductionComponent(productionRecipe));
            entity.Attach(new ResourceStorageComponent(
                (productionRecipe.Ingredients.Volume + productionRecipe.ResourceQuantity.Volume) * productionRuns));

            return entity.Id;
        }
    }
}