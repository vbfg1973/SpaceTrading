using MediatR;
using Microsoft.Extensions.Logging;
using SpaceTrading.Production.Data;

namespace SpaceTrading.Production.Domain.Features.ResourceSize.Create;

public class CreateResourceSizeCommandHandler : IRequestHandler<CreateResourceSizeCommand>
{
    private readonly SpaceTradingContext _context;
    private readonly ILogger<CreateResourceSizeCommandHandler> _logger;

    public CreateResourceSizeCommandHandler(SpaceTradingContext context, ILogger<CreateResourceSizeCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    public async Task Handle(CreateResourceSizeCommand request, CancellationToken cancellationToken)
    {
        _logger.LogTrace("{Class} {Method} {Request}", typeof(CreateResourceSizeCommandHandler), nameof(Handle), request);

        _context.ResourceSizes.Add(new Data.Models.ResourceSize()
        {
            Name = request.Name
        });

        await _context.SaveChangesAsync(cancellationToken);
    }
}