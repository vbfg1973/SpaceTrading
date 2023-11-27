using MediatR;

namespace SpaceTrading.Production.Domain.Features.ResourceSize.Create;

public record CreateResourceSizeCommand : IRequest
{
    public CreateResourceSizeCommand(string name, int sizeModifier)
    {
        Name = name;
        SizeModifier = sizeModifier;
    }

    public string Name { get; }
    public int SizeModifier { get; }
}