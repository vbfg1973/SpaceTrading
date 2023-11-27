using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Serilog;
using SpaceTrading.Production.Data;
using SpaceTrading.Production.Domain;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json");
builder.Configuration.AddEnvironmentVariables();

Log.Logger = new LoggerConfiguration()
    .ReadFrom
    .Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog((ctx, lc) =>
{
    lc
        .WriteTo
        .Console()
        .ReadFrom.Configuration(ctx.Configuration);
});

AppSettings appSettings = new();
builder.Configuration.Bind("Settings", appSettings);

builder.Services.AddAutoMapper(ProductionDomainAssemblyReference.Assembly);
builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining<ProductionDomainAssemblyReference>());


builder
    .Services
    .AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder
    .Services
    .AddDbContext<SpaceTradingContext>(dbContextOptionsBuilder =>
        dbContextOptionsBuilder.UseSqlServer(appSettings.ConnectionString));

var app = builder.Build();

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();