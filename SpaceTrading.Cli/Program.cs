using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using CsvHelper;
using SpaceTrading.Production.General.Resources;

namespace SpaceTrading.Cli
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var resources = ReadCsv<ResourcesDto>(args[0]).ToList();
            var productionRecipeFactory = new ProductionRecipeFactory(resources);
            var recipes = productionRecipeFactory.Recipes();

            Console.WriteLine(JsonSerializer.Serialize(productionRecipeFactory.Recipes(),
                new JsonSerializerOptions { WriteIndented = true, Converters = { new JsonStringEnumConverter() } }));

            var energyQuantities = EnergyQuantities(productionRecipeFactory)
                .OrderByDescending(resourceQuantity => resourceQuantity.Quantity)
                .ThenBy(resourceQuantity => resourceQuantity.Resource.Name);

            Console.WriteLine();
            Console.WriteLine("Energy required for all resources consumed by production run");
            foreach (var resourceQuantity in energyQuantities)
                Console.WriteLine(
                    $"{resourceQuantity.Resource.Name} - {resourceQuantity.Resource.Category} - {resourceQuantity.Quantity}");
        }

        private static IEnumerable<ResourceQuantity> EnergyQuantities(ProductionRecipeFactory productionRecipeFactory)
        {
            return productionRecipeFactory.Recipes().Select(r => new ResourceQuantity
            {
                Resource = r.ResourceQuantity.Resource,
                Quantity = productionRecipeFactory.GetEnergyQuantity(r.ResourceQuantity)
            });
        }

        private static IEnumerable<T> ReadCsv<T>(string fileName)
        {
            var streamReader = new StreamReader(fileName);
            var csv = new CsvReader(streamReader, CultureInfo.InvariantCulture);
            return csv.GetRecords<T>();
        }
    }
}