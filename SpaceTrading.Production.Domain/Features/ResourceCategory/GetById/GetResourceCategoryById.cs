namespace SpaceTrading.Production.Domain.Features.ResourceCategory.GetById
{
    public record GetResourceCategoryById(int Id, Guid CorrelationId) : ICommand<ResourceCategoryDto>;
}