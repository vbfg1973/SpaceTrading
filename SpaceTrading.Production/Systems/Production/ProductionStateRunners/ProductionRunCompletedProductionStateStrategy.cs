using SpaceTrading.Production.Components.ResourceProduction;
using SpaceTrading.Production.Components.ResourceStorage;

namespace SpaceTrading.Production.Systems.Production.ProductionStateRunners
{
    public class ProductionRunCompletedProductionStateStrategy : IProductionStateStrategy
    {
        private readonly ResourceProductionComponent _productionComponent;
        private readonly ResourceStorageComponent _storageComponent;

        public ProductionRunCompletedProductionStateStrategy(ResourceProductionComponent productionComponent,
            ResourceStorageComponent storageComponent)
        {
            _productionComponent = productionComponent;
            _storageComponent = storageComponent;
        }

        public void Run()
        {
            if (!_storageComponent.WillFit(_productionComponent.Recipe.Ingredients.Volume))
            {
                Console.WriteLine(
                    $"Ingredients volume ({_productionComponent.Recipe.Ingredients.Volume}) will not fit remaining storage ({_storageComponent.VolumeRemaining})");
                return;
            }

            if (!_productionComponent.TryGetCompletedResource(out var resourceQuantity))
            {
                Console.WriteLine("Cannot get completed production");
                return;
            }

            if (_storageComponent.TryAdd(resourceQuantity)) return;

            Console.WriteLine("Cannot add completed production to storage");
        }
    }
}