using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using SpaceTrading.Production.Components;
using SpaceTrading.Production.Components.ResourceProduction;
using SpaceTrading.Production.Components.ResourceProduction.StateMachine;
using SpaceTrading.Production.Components.ResourceStorage;
using SpaceTrading.Production.Systems.Production.ProductionStateRunners;

namespace SpaceTrading.Production.Systems
{
    public class ProductionSystem : EntityProcessingSystem
    {
        private ComponentMapper<ResourceProductionComponent> _productionComponentMapper = null!;
        private ComponentMapper<ResourceStorageComponent> _storageComponentMapper = null!;

        public ProductionSystem() : base(Aspect.All(typeof(ResourceProductionComponent),
            typeof(ResourceStorageComponent), typeof(ProductionFlagComponent)))
        {
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _productionComponentMapper = mapperService.GetMapper<ResourceProductionComponent>();
            _storageComponentMapper = mapperService.GetMapper<ResourceStorageComponent>();
        }

        public override void Process(GameTime gameTime, int entityId)
        {
            var production = _productionComponentMapper.Get(entityId);
            var storage = _storageComponentMapper.Get(entityId);

            if (production.CurrentState == ResourceProductionState.InProgress) return;

            var productionStateRunner = ProductionStateStrategy(production.CurrentState, production, storage);
            productionStateRunner.Run();
        }

        private static IProductionStateStrategy ProductionStateStrategy(ResourceProductionState resourceProductionState,
            ResourceProductionComponent production, ResourceStorageComponent storage)
        {
            switch (resourceProductionState)
            {
                case ResourceProductionState.ProductionRunCompleted:
                    return new ProductionRunCompletedProductionStateStrategy(production, storage);
                case ResourceProductionState.ReadyToStart:
                    return new ReadyToStartProductionStateStrategy(production, storage);
                case ResourceProductionState.InProgress:
                default:
                    throw new ArgumentOutOfRangeException(nameof(resourceProductionState), resourceProductionState,
                        null);
            }
        }
    }
}