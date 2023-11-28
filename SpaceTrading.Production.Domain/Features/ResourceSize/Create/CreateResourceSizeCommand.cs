namespace SpaceTrading.Production.Domain.Features.ResourceSize.Create
{
    public class CreateResourceSizeCommand : ICommand<ResourceSizeDto>
    {
        public CreateResourceSizeCommand(CreateResourceSizeCommandDto createResourceSizeCommandDto, Guid correlationId)
        {
            Name = createResourceSizeCommandDto.Name;
            Size = createResourceSizeCommandDto.Size;
            CorrelationId = correlationId;
        }

        public string Name { get; }
        public int Size { get; }
        public Guid CorrelationId { get; }
    }
}