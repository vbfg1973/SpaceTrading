using SpaceTrading.Production.Components.ResourceProduction;
using SpaceTrading.Production.Components.ResourceProduction.Recipes;
using SpaceTrading.Production.Components.ResourceStorage;

namespace SpaceTrading.Production.Systems.Production.ProductionStateRunners
{
    public class ReadyToStartProductionStateRunner : IProductionStateRunner
    {
        private readonly ResourceProductionComponent _productionComponent;
        private readonly ResourceStorageComponent _storageComponent;

        public ReadyToStartProductionStateRunner(ResourceProductionComponent productionComponent, ResourceStorageComponent storageComponent)
        {
            _productionComponent = productionComponent;
            _storageComponent = storageComponent;
        }
        
        public void Run()
        {
            if (!TryGetIngredientsFromStorage(out var ingredients))
                Console.WriteLine("Could not get ingredients from storage");

            if (_productionComponent.TryStartProduction(ingredients)) return;

            Console.WriteLine("Could not start production");
        }

        private bool TryGetIngredientsFromStorage(out ProductionRecipeIngredients ingredients)
        {
            ingredients = new ProductionRecipeIngredients();

            var ingredientsAvailable = _productionComponent.Recipe.Ingredients.All(_storageComponent.HasAvailable);
            if (!ingredientsAvailable) return false;

            foreach (var recipeIngredient in _productionComponent.Recipe.Ingredients)
            {
                _storageComponent.TryRemove(recipeIngredient, out var resourceQuantity);
                ingredients.Add(resourceQuantity);
            }

            return true;
        }
    }
}