using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SpaceTrading.Production.Data;

namespace SpaceTrading.Production.Domain.Features.ResourceSize.Create
{
    public class CreateResourceSizeCommandHandler : IRequestHandler<CreateResourceSizeCommand, ResourceSizeDto>
    {
        private readonly SpaceTradingContext _context;
        private readonly ILogger<CreateResourceSizeCommandHandler> _logger;
        private readonly IMapper _mapper;

        public CreateResourceSizeCommandHandler(SpaceTradingContext context, IMapper mapper,
            ILogger<CreateResourceSizeCommandHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ResourceSizeDto> Handle(CreateResourceSizeCommand request,
            CancellationToken cancellationToken)
        {
            _logger.LogTrace("{Class} {Method} {Request}", typeof(CreateResourceSizeCommandHandler), nameof(Handle),
                request);

            var model = _mapper.Map<Data.Models.ResourceSize>(request);
            
            _context.ResourceSizes.Add(model);

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ResourceSizeDto>(await _context.ResourceSizes.Where(x => x.Name == request.Name)
                .FirstAsync(cancellationToken));
        }
    }
}