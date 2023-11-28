namespace SpaceTrading.Production.Domain.Features.ResourceSize.Update
{
    public class UpdateResourceSizeCommand : ICommand<ResourceSizeDto>
    {
        public UpdateResourceSizeCommand(int id, UpdateResourceSizeDto updateResourceSizeDto, Guid correlationId)
        {
            Id = id;
            Name = updateResourceSizeDto.Name;
            Size = updateResourceSizeDto.Size;

            CorrelationId = correlationId;
        }

        public int Id { get; init; }
        public string Name { get; init; }
        public int Size { get; init; }
        public Guid CorrelationId { get; }
    }
}