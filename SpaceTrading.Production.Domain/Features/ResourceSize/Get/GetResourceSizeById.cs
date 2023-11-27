using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SpaceTrading.Production.Data;

namespace SpaceTrading.Production.Domain.Features.ResourceSize.Get
{
    public record GetResourceSizeById(int Id) : IRequest<ResourceSizeDto>;

    public class GetResourceSizeByIdHandler : IRequestHandler<GetResourceSizeById, ResourceSizeDto>
    {
        private readonly SpaceTradingContext _context;
        private readonly ILogger<GetResourceSizeByIdHandler> _logger;
        private readonly IMapper _mapper;

        public GetResourceSizeByIdHandler(SpaceTradingContext context, IMapper mapper,
            ILogger<GetResourceSizeByIdHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ResourceSizeDto> Handle(GetResourceSizeById request, CancellationToken cancellationToken)
        {
            return _mapper.Map<ResourceSizeDto>(
                await _context.ResourceSizes.FirstAsync(x => x.Id == request.Id, cancellationToken));
        }
    }
}