using System.Text.Json;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SpaceTrading.Production.Data;
using SpaceTrading.Production.Domain.Exceptions;

namespace SpaceTrading.Production.Domain.Features.ResourceCategory.Update
{
    public class UpdateResourceCategoryCommandHandler : IRequestHandler<UpdateResourceCategoryCommand, ResourceCategoryDto>
    {
        private readonly SpaceTradingContext _context;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateResourceCategoryCommandHandler> _logger;

        public UpdateResourceCategoryCommandHandler(SpaceTradingContext context, IMediator mediator, IMapper mapper,
            ILogger<UpdateResourceCategoryCommandHandler> logger)
        {
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ResourceCategoryDto> Handle(UpdateResourceCategoryCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("{Class} {Method} {Json} {CorrelationId}", typeof(UpdateResourceCategoryCommand), nameof(Handle), JsonSerializer.Serialize(request), request.CorrelationId);

            await _context.Database.BeginTransactionAsync(cancellationToken);
            
            if (!await _context.ResourceCategorys.AnyAsync(x => x.Id == request.Id, cancellationToken: cancellationToken))
                throw new NotFoundException(typeof(Data.Models.ResourceCategory), request.Id);

            var model = _mapper.Map<Data.Models.ResourceCategory>(request);

            _context.Update(model);
            await _context.SaveChangesAsync(cancellationToken);

            await _context.Database.CommitTransactionAsync(cancellationToken);

            return _mapper.Map<ResourceCategoryDto>(model);
        }
    }
}