namespace SpaceTrading.Production.Domain.Features.ResourceCategory.Update
{
    public class UpdateResourceCategoryCommand : ICommand<ResourceCategoryDto>
    {
        public UpdateResourceCategoryCommand(int id, UpdateResourceCategoryDto updateResourceCategoryDto, Guid correlationId)
        {
            Id = id;
            Name = updateResourceCategoryDto.Name;

            CorrelationId = correlationId;
        }

        public int Id { get; init; }
        public string Name { get; init; }
        public Guid CorrelationId { get; }
    }
}