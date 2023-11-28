using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SpaceTrading.Production.Data;
using SpaceTrading.Production.Domain.Exceptions;

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
            
            await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            
            var model = _mapper.Map<Data.Models.ResourceSize>(request);

            if (await _context.ResourceSizes.AnyAsync(x => x.Name == request.Name, cancellationToken: cancellationToken))
            {
                throw new AlreadyExistsException(typeof(Data.Models.ResourceSize), request.Name);
            }

            _context.ResourceSizes.Add(model);

            await _context.SaveChangesAsync(cancellationToken);
            var returnedModel = await _context.ResourceSizes.Where(x => x.Name == request.Name)
                .FirstAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            return _mapper.Map<ResourceSizeDto>(returnedModel);
        }
    }
}