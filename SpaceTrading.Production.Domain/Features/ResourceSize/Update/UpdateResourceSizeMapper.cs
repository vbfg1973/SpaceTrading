using AutoMapper;

namespace SpaceTrading.Production.Domain.Features.ResourceSize.Update
{
    public class UpdateResourceSizeMapper : Profile
    {
        public UpdateResourceSizeMapper()
        {
            CreateMap<UpdateResourceSizeCommand, Data.Models.ResourceSize>();
        }
    }
}