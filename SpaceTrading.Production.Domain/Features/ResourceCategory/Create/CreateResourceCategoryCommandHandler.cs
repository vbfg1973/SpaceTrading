using System.Text.Json;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SpaceTrading.Production.Data;
using SpaceTrading.Production.Domain.Exceptions;

namespace SpaceTrading.Production.Domain.Features.ResourceCategory.Create
{
    public class CreateResourceCategoryCommandHandler : IRequestHandler<CreateResourceCategoryCommand, ResourceCategoryDto>
    {
        private readonly SpaceTradingContext _context;
        private readonly ILogger<CreateResourceCategoryCommandHandler> _logger;
        private readonly IMapper _mapper;

        public CreateResourceCategoryCommandHandler(SpaceTradingContext context, IMapper mapper,
            ILogger<CreateResourceCategoryCommandHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ResourceCategoryDto> Handle(CreateResourceCategoryCommand command,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("{Class} {Method} {Request}", 
                typeof(CreateResourceCategoryCommandHandler),
                nameof(Handle),
                JsonSerializer.Serialize(command),
                command.CorrelationId);

            await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

            var model = _mapper.Map<Data.Models.ResourceCategory>(command);

            if (await _context.ResourceCategorys.AnyAsync(x => x.Name == command.Name, cancellationToken))
                throw new AlreadyExistsException(typeof(Data.Models.ResourceCategory), command.Name);

            _context.ResourceCategorys.Add(model);

            await _context.SaveChangesAsync(cancellationToken);
            var returnedModel = await _context.ResourceCategorys.Where(x => x.Name == command.Name)
                .FirstAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            return _mapper.Map<ResourceCategoryDto>(returnedModel);
        }
    }
}