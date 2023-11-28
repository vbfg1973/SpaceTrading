namespace SpaceTrading.Production.Domain.Features.ResourceSize.GetById
{
    public record GetResourceSizeById(int Id, Guid CorrelationId) : ICommand<ResourceSizeDto>;
}