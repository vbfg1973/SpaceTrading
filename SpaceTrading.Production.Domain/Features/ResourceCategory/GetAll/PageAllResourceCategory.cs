namespace SpaceTrading.Production.Domain.Features.ResourceCategory.GetAll
{
    public record PageAllResourceCategory(PageParameters PageParameters, Guid CorrelationId) : ICommand<PagedList<ResourceCategoryDto>>;
}

