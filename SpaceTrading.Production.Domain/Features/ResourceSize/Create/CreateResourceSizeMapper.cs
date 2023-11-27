using AutoMapper;

namespace SpaceTrading.Production.Domain.Features.ResourceSize.Create;

public class CreateResourceSizeMapper : Profile
{
    public CreateResourceSizeMapper()
    {
        CreateMap<CreateResourceSizeDto, CreateResourceSizeCommand>();
    }
}