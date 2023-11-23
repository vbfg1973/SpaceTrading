using SpaceTrading.Production.General.Resources;

namespace SpaceTrading.Cli;

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
    public string? R4Name { get; set; } = null!;
    public int? R4Quantity { get; set; }
}