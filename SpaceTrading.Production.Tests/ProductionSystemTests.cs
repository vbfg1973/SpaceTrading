using System.Text.Json;
using System.Text.Json.Serialization;
using FluentAssertions;
using SpaceTrading.Production.Components.ResourceProduction;
using SpaceTrading.Production.Components.ResourceProduction.Recipes;
using SpaceTrading.Production.Components.ResourceProduction.StateMachine;
using SpaceTrading.Production.Components.ResourceStorage;
using SpaceTrading.Production.General.Resources;
using SpaceTrading.Production.Systems;

namespace SpaceTrading.Production.Tests
{
    public class ProductionSystemTests
    {
        [Theory]
        [InlineData("axe")]
        [InlineData("energy")]
        [InlineData("iron")]
        [InlineData("ore")]
        [InlineData("wood")]
        public void GivenResourceName_NameOfResourceInRecipeMatches(string recipeResourceName)
        {
            var recipe = ReadProductionRecipeFromResourceName(recipeResourceName);

            recipe.ResourceQuantity.Resource.Name.Equals(recipeResourceName,
                    StringComparison.InvariantCultureIgnoreCase)
                .Should()
                .BeTrue();
        }

        [Theory]
        [InlineData("axe")]
        [InlineData("energy")]
        [InlineData("iron")]
        [InlineData("ore")]
        [InlineData("wood")]
        public void GivenRecipeNewProductionComponentsShouldBeReadyToStart(string recipeResourceName)
        {
            var recipe = ReadProductionRecipeFromResourceName(recipeResourceName);

            var productionComponent = new ResourceProductionComponent(recipe);
            var storageComponent = new ResourceStorageComponent(productionComponent.Recipe.SingleRunVolumeRequired);

            productionComponent.Update(0.1717f);
            productionComponent.CurrentState.Should().Be(ResourceProductionState.ReadyToStart);
        }

        [Theory]
        [InlineData("axe")]
        [InlineData("energy")]
        [InlineData("iron")]
        [InlineData("ore")]
        [InlineData("wood")]
        public void GivenRecipeNewProductionComponentsWithSomeTimeElapsedShouldBeInProgress(string recipeResourceName)
        {
            var recipe = ReadProductionRecipeFromResourceName(recipeResourceName);

            var productionComponent = new ResourceProductionComponent(recipe);
            var storageComponent =
                new ResourceStorageComponent(productionComponent.Recipe.SingleRunVolumeRequired * 100);
            foreach (var rq in recipe.Ingredients)
                storageComponent.TryAdd(new ResourceQuantity { Resource = rq.Resource, Quantity = rq.Quantity * 10 });

            ProductionSystem.ProcessComponents(1f, productionComponent, storageComponent);

            productionComponent.CurrentState.Should().Be(ResourceProductionState.InProgress);
        }

        [Theory]
        [InlineData("axe")]
        [InlineData("energy")]
        [InlineData("iron")]
        [InlineData("ore")]
        [InlineData("wood")]
        public void
            GivenRecipeNewProductionComponentsWithAllTimeElapsedAndStorageAvailableForCompletedProductionShouldBeReadyToStart(
                string recipeResourceName)
        {
            var recipe = ReadProductionRecipeFromResourceName(recipeResourceName);

            var productionComponent = new ResourceProductionComponent(recipe);
            var storageComponent =
                new ResourceStorageComponent(productionComponent.Recipe.SingleRunVolumeRequired * 100);
            foreach (var rq in recipe.Ingredients)
                storageComponent.TryAdd(new ResourceQuantity { Resource = rq.Resource, Quantity = rq.Quantity * 10 });

            ProductionSystem.ProcessComponents(1f, productionComponent, storageComponent);
            ProductionSystem.ProcessComponents(recipe.TimeTaken, productionComponent, storageComponent);

            productionComponent.CurrentState.Should().Be(ResourceProductionState.ReadyToStart);
        }

        private static ProductionRecipe ReadProductionRecipeFromResourceName(string recipeResourceName)
        {
            var recipeJson =
                File.ReadAllText(Path.Combine("Data", "Recipes", string.Join(".", recipeResourceName, "json")));
            var recipe = JsonSerializer.Deserialize<ProductionRecipe>(recipeJson,
                new JsonSerializerOptions
                    { PropertyNameCaseInsensitive = true, Converters = { new JsonStringEnumConverter() } })!;
            return recipe;
        }
    }
}