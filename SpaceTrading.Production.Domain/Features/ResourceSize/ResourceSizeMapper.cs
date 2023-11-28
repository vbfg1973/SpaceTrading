using AutoMapper;
using SpaceTrading.Production.Domain.Features.ResourceSize.Create;

namespace SpaceTrading.Production.Domain.Features.ResourceSize
{
    public class ResourceSizeMapper : Profile
    {
        public ResourceSizeMapper()
        {
            CreateMap<Data.Models.ResourceSize, ResourceSizeDto>();
            CreateMap<ResourceSizeDto, Data.Models.ResourceSize>();
        }
    }
}