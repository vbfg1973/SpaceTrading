﻿using MediatR;

namespace SpaceTrading.Production.Domain.Features.ResourceSize.Create
{
    public class CreateResourceSizeCommand : IRequest<ResourceSizeDto>
    {
        public string Name { get; init; } = null!;
        public int Size { get; init; }
    }
}