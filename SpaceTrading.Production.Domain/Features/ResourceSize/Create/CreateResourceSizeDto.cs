namespace SpaceTrading.Production.Domain.Features.ResourceSize.Create;

public class CreateResourceSizeDto
{
    public string Size { get; set; } = null!;
    public int SizeModifier { get; set; }
}