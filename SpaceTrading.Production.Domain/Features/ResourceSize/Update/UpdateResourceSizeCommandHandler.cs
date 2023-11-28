using System.Text.Json;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SpaceTrading.Production.Data;
using SpaceTrading.Production.Domain.Exceptions;

namespace SpaceTrading.Production.Domain.Features.ResourceSize.Update
{
    public class UpdateResourceSizeCommandHandler : IRequestHandler<UpdateResourceSizeCommand, ResourceSizeDto>
    {
        private readonly SpaceTradingContext _context;
        private readonly ILogger<UpdateResourceSizeCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public UpdateResourceSizeCommandHandler(SpaceTradingContext context, IMediator mediator, IMapper mapper,
            ILogger<UpdateResourceSizeCommandHandler> logger)
        {
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ResourceSizeDto> Handle(UpdateResourceSizeCommand request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("{Class} {Method} {Json} {CorrelationId}", typeof(UpdateResourceSizeCommand),
                nameof(Handle), JsonSerializer.Serialize(request), request.CorrelationId);

            await _context.Database.BeginTransactionAsync(cancellationToken);

            if (!await _context.ResourceSizes.AnyAsync(x => x.Id == request.Id, cancellationToken))
                throw new NotFoundException(typeof(Data.Models.ResourceSize), request.Id);

            var model = await _context
                .ResourceSizes
                .AsNoTracking()
                .FirstAsync(resourceSize => resourceSize.Id == request.Id, cancellationToken);

            model = _mapper.Map<Data.Models.ResourceSize>(request);

            _context.Update(model);
            await _context.SaveChangesAsync(cancellationToken);

            await _context.Database.CommitTransactionAsync(cancellationToken);

            return _mapper.Map<ResourceSizeDto>(model);
        }
    }
}