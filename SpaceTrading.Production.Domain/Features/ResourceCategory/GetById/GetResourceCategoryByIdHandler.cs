using System.Text.Json;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SpaceTrading.Production.Data;
using SpaceTrading.Production.Domain.Exceptions;
using SpaceTrading.Production.Domain.Features.ResourceCategory.GetById;

namespace SpaceTrading.Production.Domain.Features.ResourceCategory.Get
{
    public class GetResourceCategoryByIdHandler : IRequestHandler<GetResourceCategoryById, ResourceCategoryDto>
    {
        private readonly SpaceTradingContext _context;
        private readonly ILogger<GetResourceCategoryByIdHandler> _logger;
        private readonly IMapper _mapper;

        public GetResourceCategoryByIdHandler(SpaceTradingContext context, IMapper mapper,
            ILogger<GetResourceCategoryByIdHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ResourceCategoryDto> Handle(GetResourceCategoryById request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("{Class} {Method} {Json} {CorrelationId}",
                typeof(GetResourceCategoryByIdHandler),
                nameof(Handle),
                JsonSerializer.Serialize(request),
                request.CorrelationId
            );

            var result = await _context.ResourceCategorys.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (result != null)
            {
                return _mapper.Map<ResourceCategoryDto>(result);
            }

            throw new NotFoundException(typeof(Data.Models.ResourceCategory), request.Id);
        }
    }
}