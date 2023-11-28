using AutoMapper;

namespace SpaceTrading.Production.Domain.Features.ResourceCategory.Update
{
    public class UpdateResourceCategoryMapper : Profile
    {
        public UpdateResourceCategoryMapper()
        {
            CreateMap<UpdateResourceCategoryCommand, Data.Models.ResourceCategory>();
        }
    }
}