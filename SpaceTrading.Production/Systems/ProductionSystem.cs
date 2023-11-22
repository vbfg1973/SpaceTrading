using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using SpaceTrading.Production.Components;
using SpaceTrading.Production.Components.ResourceProduction;
using SpaceTrading.Production.Components.ResourceProduction.Recipes;
using SpaceTrading.Production.Components.ResourceProduction.StateMachine;
using SpaceTrading.Production.Components.ResourceStorage;

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

            switch (production.CurrentState)
            {
                case ResourceProductionState.InProgress:
                    break;
                case ResourceProductionState.ProductionRunCompleted:
                    ProductionRunCompleted(production, storage);
                    break;
                case ResourceProductionState.ReadyToStart:
                    ReadyToStart(production, storage);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static void ReadyToStart(ResourceProductionComponent production,
            ResourceStorageComponent storage)
        {
            if (!TryGetIngredientsFromStorage(production, storage, out var ingredients))
                Console.WriteLine("Could not get ingredients from storage");

            if (production.TryStartProduction(ingredients)) return;

            Console.WriteLine("Could not start production");
        }

        private static bool TryGetIngredientsFromStorage(ResourceProductionComponent production,
            ResourceStorageComponent storage, out ProductionRecipeIngredients ingredients)
        {
            ingredients = new ProductionRecipeIngredients();

            var ingredientsAvailable = production.Recipe.Ingredients.All(storage.HasAvailable);
            if (!ingredientsAvailable) return false;

            foreach (var recipeIngredient in production.Recipe.Ingredients)
            {
                storage.TryRemove(recipeIngredient, out var resourceQuantity);
                ingredients.Add(resourceQuantity);
            }

            return true;
        }

        private static void ProductionRunCompleted(ResourceProductionComponent production,
            ResourceStorageComponent storage)
        {
            if (!storage.WillFit(production.Recipe.Ingredients.Volume))
            {
                Console.WriteLine(
                    $"Ingredients volume ({production.Recipe.Ingredients.Volume}) will not fit remaining storage ({storage.VolumeRemaining})");
                return;
            }

            if (!production.TryGetCompletedResource(out var resourceQuantity))
            {
                Console.WriteLine("Cannot get completed production");
                return;
            }

            if (storage.TryAdd(resourceQuantity)) return;

            Console.WriteLine("Cannot add completed production to storage");
        }
    }
}