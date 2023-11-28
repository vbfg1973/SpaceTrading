using MediatR;

namespace SpaceTrading.Production.Domain.Features.ResourceSize.Get
{
    public record GetResourceSizeById(int Id) : IRequest<ResourceSizeDto>;
}