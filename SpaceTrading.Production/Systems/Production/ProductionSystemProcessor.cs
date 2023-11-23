using MonoGame.Extended.Entities;
using SpaceTrading.Production.Components.ResourceProduction;
using SpaceTrading.Production.Components.ResourceProduction.StateMachine;
using SpaceTrading.Production.Components.ResourceStorage;

namespace SpaceTrading.Production.Systems.Production;

public class ProductionSystemProcessor
{
    private readonly int _entityId;
    private readonly IComponentMapperService _mapperService;
    private readonly ResourceProductionComponent _productionComponent;
    private readonly ResourceStorageComponent _storageComponent;

    public ProductionSystemProcessor(IComponentMapperService mapperService, int entityId)
    {
        _mapperService = mapperService;
        _entityId = entityId;

        _productionComponent = _mapperService.GetMapper<ResourceProductionComponent>().Get(entityId);
        _storageComponent = _mapperService.GetMapper<ResourceStorageComponent>().Get(entityId);
    }

    public void Run(float elapsedSeconds)
    {
        ProcessComponents(elapsedSeconds, _productionComponent, _storageComponent);
    }

    public static void ProcessComponents(float elapsedSeconds, ResourceProductionComponent production,
        ResourceStorageComponent storage)
    {
        production.Update(elapsedSeconds);

        if (production.CurrentState == ResourceProductionState.InProgress) return;

        var productionStateRunner = ProductionStateStrategyFactory.Create(production, storage);
        productionStateRunner.Run();
    }
}