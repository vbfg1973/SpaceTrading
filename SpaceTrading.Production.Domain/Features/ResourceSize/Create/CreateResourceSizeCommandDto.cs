namespace SpaceTrading.Production.Domain.Features.ResourceSize.Create
{
    public class CreateResourceSizeCommandDto
    {
        public string Name { get; init; } = null!;
        public int Size { get; init; }
    }
}