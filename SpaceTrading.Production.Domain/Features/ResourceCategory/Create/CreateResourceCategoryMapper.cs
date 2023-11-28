using AutoMapper;

namespace SpaceTrading.Production.Domain.Features.ResourceCategory.Create
{
    public class CreateResourceCategoryMapper : Profile
    {
        public CreateResourceCategoryMapper()
        {
            CreateMap<CreateResourceCategoryCommand, Data.Models.ResourceCategory>();
        }
    }
}