namespace SpaceTrading.Production.Domain.Features.ResourceCategory.Create
{
    public class CreateResourceCategoryCommand : ICommand<ResourceCategoryDto>
    {
        public CreateResourceCategoryCommand(CreateResourceCategoryCommandDto createResourceCategoryCommandDto, Guid correlationId)
        {
            Name = createResourceCategoryCommandDto.Name;
            CorrelationId = correlationId;
        }

        public string Name { get; }

        public Guid CorrelationId { get; }
    }
}