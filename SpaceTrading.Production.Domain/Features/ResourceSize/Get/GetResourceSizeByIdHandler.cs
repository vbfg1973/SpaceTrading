using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SpaceTrading.Production.Data;
using SpaceTrading.Production.Domain.Exceptions;

namespace SpaceTrading.Production.Domain.Features.ResourceSize.Get
{
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
            if (!await _context.ResourceSizes.AnyAsync(x => x.Id == request.Id, cancellationToken: cancellationToken))
                throw new NotFoundException(typeof(Data.Models.ResourceSize), request.Id);
            
            return _mapper.Map<ResourceSizeDto>(
                await _context.ResourceSizes.FirstAsync(x => x.Id == request.Id, cancellationToken));
        }
    }
}