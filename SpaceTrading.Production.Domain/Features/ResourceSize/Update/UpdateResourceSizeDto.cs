namespace SpaceTrading.Production.Domain.Features.ResourceSize.Update
{
    public class UpdateResourceSizeDto
    {
        public string Name { get; init; } = null!;
        public int Size { get; init; }
    }
}