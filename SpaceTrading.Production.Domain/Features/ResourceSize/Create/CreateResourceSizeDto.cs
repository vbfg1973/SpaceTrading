namespace SpaceTrading.Production.Domain.Features.ResourceSize.Create
{
    public class CreateResourceSizeDto
    {
        public string Name { get; set; } = null!;
        public int Size { get; set; }
    }
}