using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using CsvHelper;
using SpaceTrading.Production.Components.ResourceProduction.Recipes;
using SpaceTrading.Production.General.Resources;

namespace SpaceTrading.Cli
{
    public class ResourcesDto
    {
        public string Name { get; set; } = null!;
        public ResourceCategory Class { get; set; }
        public ResourceSize CargoClass { get; set; }
        public int UnitVolume { get; set; }
        public int ProductionOutput { get; set; }
        public int ProductionRunTime { get; set; }
        public string? R1Name { get; set; } = null!;
        public int? R1Quantity { get; set; }
        public string? R2Name { get; set; } = null!;
        public int? R2Quantity { get; set; }
        public string? R3Name { get; set; } = null!;
        public int? R3Quantity { get; set; }
        // public string? R4Name { get; set; } = null!;
        // public int? R4Quantity { get; set; }
    }

    public static class Program
    {
        private static Dictionary<string, Resource> _resources = new();

        public static void Main(string[] args)
        {
            var resources = ReadCsv<ResourcesDto>(args[0]).ToList();

            foreach (var r in resources)
            {
                _resources[r.Name] = OutputResource(r);
            }
            
            
            
            Console.WriteLine(JsonSerializer.Serialize(Recipes(resources), new JsonSerializerOptions() {WriteIndented = true, Converters = { new JsonStringEnumConverter() }}));
        }

        private static IEnumerable<ProductionRecipe> Recipes(IEnumerable<ResourcesDto> resources)
        {
            return resources.Select(resourcesDto => new ProductionRecipe(RecipeOutputResourceQuantity(resourcesDto), Ingredients(resourcesDto), resourcesDto.ProductionRunTime));
        }

        private static Resource OutputResource(ResourcesDto resourcesDto)
        {
            return new Resource(resourcesDto.Name, resourcesDto.Class, resourcesDto.CargoClass);
        }

        private static ResourceQuantity RecipeOutputResourceQuantity(ResourcesDto resourcesDto)
        {
            return new ResourceQuantity()
            {
                Quantity = resourcesDto.ProductionOutput,
                Resource = OutputResource(resourcesDto)
            };  
        }

        private static ProductionRecipeIngredients Ingredients(ResourcesDto resourcesDto)
        {
            var ing = new ProductionRecipeIngredients();

            AddResourceQuantity(ing, resourcesDto.R1Name, resourcesDto.R1Quantity);
            AddResourceQuantity(ing, resourcesDto.R2Name, resourcesDto.R2Quantity);
            AddResourceQuantity(ing, resourcesDto.R3Name, resourcesDto.R3Quantity);

            return ing;
        }

        private static void AddResourceQuantity(ProductionRecipeIngredients ing, string? resourceName, int? quantity)
        {
            if (!string.IsNullOrEmpty(resourceName) && quantity.HasValue)
            {
                ing.Add(new ResourceQuantity()
                {
                    Resource = _resources[resourceName],
                    Quantity = quantity.Value
                });
            }
        }
        
        private static IEnumerable<T> ReadCsv<T>(string fileName)
        {
            var streamReader = new StreamReader(fileName);
            var csv = new CsvReader(streamReader, CultureInfo.InvariantCulture);
            return csv.GetRecords<T>();
        }
    }
}