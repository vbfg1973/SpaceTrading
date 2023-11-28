using AutoMapper;
using SpaceTrading.Production.Domain.Features.ResourceCategory.Create;

namespace SpaceTrading.Production.Domain.Features.ResourceCategory
{
    public class ResourceCategoryMapper : Profile
    {
        public ResourceCategoryMapper()
        {
            CreateMap<Data.Models.ResourceCategory, ResourceCategoryDto>();
            CreateMap<ResourceCategoryDto, Data.Models.ResourceCategory>();
        }
    }
}