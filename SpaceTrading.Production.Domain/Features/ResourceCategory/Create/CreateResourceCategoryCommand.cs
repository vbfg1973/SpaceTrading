namespace SpaceTrading.Production.Domain.Features.ResourceCategory.Create
{
    public class CreateResourceCategoryCommand : ICommand<ResourceCategoryDto>
    {
        public CreateResourceCategoryCommand(CreateResourceCategoryCommandDto createResourceCategoryCommandDto, Guid correlationId)
        {
            Name = createResourceCategoryCommandDto.Name;
            Size = createResourceCategoryCommandDto.Size;
            CorrelationId = correlationId;
        }

        public string Name { get; }
        public int Size { get; }
        public Guid CorrelationId { get; }
    }
}