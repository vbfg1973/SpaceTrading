using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using CsvHelper;

namespace SpaceTrading.Cli;

public static class Program
{
    public static void Main(string[] args)
    {
        var resources = ReadCsv<ResourcesDto>(args[0]).ToList();
        var productionRecipeFactory = new ProductionRecipeFactory(resources);
        var recipes = productionRecipeFactory.Recipes();
        
        Console.WriteLine(JsonSerializer.Serialize(productionRecipeFactory.Recipes(),
            new JsonSerializerOptions { WriteIndented = true, Converters = { new JsonStringEnumConverter() } }));

        foreach (var r in productionRecipeFactory.Recipes())
        {
            var energyQuantity = productionRecipeFactory.GetEnergyQuantity(r.ResourceQuantity);

            Console.WriteLine($"{r.ResourceQuantity.Resource.Name} - {energyQuantity}"); ;
        }
    }

    private static IEnumerable<T> ReadCsv<T>(string fileName)
    {
        var streamReader = new StreamReader(fileName);
        var csv = new CsvReader(streamReader, CultureInfo.InvariantCulture);
        return csv.GetRecords<T>();
    }
}