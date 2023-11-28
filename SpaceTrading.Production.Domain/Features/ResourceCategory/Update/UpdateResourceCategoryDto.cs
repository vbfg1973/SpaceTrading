namespace SpaceTrading.Production.Domain.Features.ResourceCategory.Update
{
    public class UpdateResourceCategoryDto
    {
        public string Name { get; init; } = null!;
        public int Size { get; init; }
    }
}