using System.Text.Json;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SpaceTrading.Production.Data;
using SpaceTrading.Production.Domain.Exceptions;
using SpaceTrading.Production.Domain.Features.ResourceSize.GetById;

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
            _logger.LogInformation("{Class} {Method} {Json} {CorrelationId}",
                typeof(GetResourceSizeByIdHandler),
                nameof(Handle),
                JsonSerializer.Serialize(request),
                request.CorrelationId
            );

            var result = await _context.ResourceSizes.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (result != null)
            {
                return _mapper.Map<ResourceSizeDto>(result);
            }

            throw new NotFoundException(typeof(Data.Models.ResourceSize), request.Id);
        }
    }
}