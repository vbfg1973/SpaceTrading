namespace SpaceTrading.Production.Domain.Features.ResourceSize.GetAll
{
    public record PageAllResourceSize(PageParameters PageParameters, Guid CorrelationId) : ICommand<PagedList<ResourceSizeDto>>;
}

