using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SpaceTrading.Production.Data;

namespace SpaceTrading.Production.Domain.Features.ResourceSize.GetAll
{
    public record PageAllResourceSize(PageParameters PageParameters, Guid CorrelationId) : ICommand<PagedList<ResourceSizeDto>>;

    public class PageAllResourceSizeHandler : IRequestHandler<PageAllResourceSize, PagedList<ResourceSizeDto>>
    {
        private readonly SpaceTradingContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<PageAllResourceSizeHandler> _logger;

        public PageAllResourceSizeHandler(SpaceTradingContext context, IMapper mapper, ILogger<PageAllResourceSizeHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }
        
        public async Task<PagedList<ResourceSizeDto>> Handle(PageAllResourceSize request, CancellationToken cancellationToken)
        {
            var queryable = _context.ResourceSizes.OrderBy(x => x.Id);

            return await PagedList<ResourceSizeDto>.ToPagedList(_mapper.ProjectTo<ResourceSizeDto>(queryable), request.PageParameters);
        }
    }
}

