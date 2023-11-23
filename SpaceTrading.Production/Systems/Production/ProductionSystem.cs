using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using SpaceTrading.Production.Components;
using SpaceTrading.Production.Components.ResourceProduction;
using SpaceTrading.Production.Components.ResourceProduction.StateMachine;
using SpaceTrading.Production.Components.ResourceStorage;
using SpaceTrading.Production.Systems.Production.ProductionStateRunners;

namespace SpaceTrading.Production.Systems.Production;

public static class ProductionStateStrategyFactory
{
    public static IProductionStateStrategy Create(ResourceProductionComponent production,
        ResourceStorageComponent storage)
    {
        switch (production.CurrentState)
        {
            case ResourceProductionState.ProductionRunCompleted:
                return new ProductionRunCompletedProductionStateStrategy(production, storage);
            case ResourceProductionState.ReadyToStart:
                return new ReadyToStartProductionStateStrategy(production, storage);
            case ResourceProductionState.InProgress:
            default:
                throw new ArgumentOutOfRangeException(nameof(production.CurrentState), production.CurrentState,
                    null);
        }
    }
}

public class ProductionSystem : EntityProcessingSystem
{
    private IComponentMapperService _mapperService;

    public ProductionSystem() : base(Aspect.All(typeof(ResourceProductionComponent),
        typeof(ResourceStorageComponent), typeof(ProductionFlagComponent))) { }

    public override void Initialize(IComponentMapperService mapperService)
    {
        _mapperService = mapperService;
    }

    public override void Process(GameTime gameTime, int entityId)
    {
        new ProductionSystemProcessor(_mapperService, entityId);
    }
}