using SpaceTrading.Production.Components.ResourceProduction.Recipes;
using SpaceTrading.Production.General.Resources;

namespace SpaceTrading.Cli
{
    public class ProductionRecipeFactory
    {
        private readonly Dictionary<string, ProductionRecipe> _recipes = new();
        private readonly Dictionary<string, Resource> _resources = new();
        private readonly List<ResourcesDto> _resourcesList;

        public ProductionRecipeFactory(IEnumerable<ResourcesDto> resources)
        {
            _resourcesList = resources.ToList();

            foreach (var r in _resourcesList) _resources[r.Name] = OutputResource(r);

            _recipes = GenerateRecipes()
                .ToDictionary(x => x.ResourceQuantity.Resource.Name, x => x);
        }

        public float GetEnergyQuantity(ResourceQuantity resourceQuantity)
        {
            var recipe = _recipes[resourceQuantity.Resource.Name];
            recipe.TryGetScaledIngredients(resourceQuantity, out var scaledIngredients);

            var energySum = scaledIngredients.Where(x => x.Resource.Name == "Energy").Sum(x => x.Quantity);
            var othersSum = scaledIngredients.Where(x => x.Resource.Name != "Energy").Sum(GetEnergyQuantity);

            return energySum + othersSum;
        }

        public IEnumerable<ProductionRecipe> Recipes()
        {
            return _recipes.Values;
        }

        private IEnumerable<ProductionRecipe> GenerateRecipes()
        {
            return _resourcesList.Select(ProductionRecipe);
        }

        private ProductionRecipe ProductionRecipe(ResourcesDto resourcesDto)
        {
            return new ProductionRecipe(RecipeOutputResourceQuantity(resourcesDto), Ingredients(resourcesDto),
                resourcesDto.ProductionRunTime);
        }

        private Resource OutputResource(ResourcesDto resourcesDto)
        {
            return new Resource(resourcesDto.Name, resourcesDto.Class, resourcesDto.CargoClass);
        }

        private ResourceQuantity RecipeOutputResourceQuantity(ResourcesDto resourcesDto)
        {
            return new ResourceQuantity
            {
                Quantity = resourcesDto.ProductionOutput,
                Resource = OutputResource(resourcesDto)
            };
        }

        private ProductionRecipeIngredients Ingredients(ResourcesDto resourcesDto)
        {
            var ing = new ProductionRecipeIngredients();

            AddResourceQuantity(ing, resourcesDto.R1Name, resourcesDto.R1Quantity);
            AddResourceQuantity(ing, resourcesDto.R2Name, resourcesDto.R2Quantity);
            AddResourceQuantity(ing, resourcesDto.R3Name, resourcesDto.R3Quantity);
            AddResourceQuantity(ing, resourcesDto.R4Name, resourcesDto.R4Quantity);
            AddResourceQuantity(ing, resourcesDto.R5Name, resourcesDto.R5Quantity);

            return ing;
        }

        private void AddResourceQuantity(ProductionRecipeIngredients ing, string? resourceName, int? quantity)
        {
            if (!string.IsNullOrEmpty(resourceName) && quantity.HasValue)
                ing.Add(new ResourceQuantity
                {
                    Resource = _resources[resourceName],
                    Quantity = quantity.Value
                });
        }
    }
}