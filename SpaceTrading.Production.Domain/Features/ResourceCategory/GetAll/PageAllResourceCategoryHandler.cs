using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SpaceTrading.Production.Data;

namespace SpaceTrading.Production.Domain.Features.ResourceCategory.GetAll
{
    public class PageAllResourceCategoryHandler : IRequestHandler<PageAllResourceCategory, PagedList<ResourceCategoryDto>>
    {
        private readonly SpaceTradingContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<PageAllResourceCategoryHandler> _logger;

        public PageAllResourceCategoryHandler(SpaceTradingContext context, IMapper mapper, ILogger<PageAllResourceCategoryHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }
        
        public async Task<PagedList<ResourceCategoryDto>> Handle(PageAllResourceCategory request, CancellationToken cancellationToken)
        {
            var queryable = _context.ResourcesCategories.OrderBy(x => x.Id);

            return await PagedList<ResourceCategoryDto>.ToPagedList(_mapper.ProjectTo<ResourceCategoryDto>(queryable), request.PageParameters);
        }
    }
}