using System.Text.Json;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SpaceTrading.Production.Domain.Features.ResourceSize.Create;
using SpaceTrading.Production.Domain.Features.ResourceSize.Get;

namespace SpaceTrading.Production.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ResourceSizeController : ControllerBase
    {
        private readonly ILogger<ResourceSizeController> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ResourceSizeController(IMapper mapper, IMediator mediator, ILogger<ResourceSizeController> logger)
        {
            _mapper = mapper;
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetResourceSize(int id)
        {
            var dto = await _mediator.Send(new GetResourceSizeById(id));
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> PostResourceSize(CreateResourceSizeDto resourceSizeDto)
        {
            _logger.LogInformation("{Class} {Method} {Json}", nameof(PostResourceSize), typeof(ResourceSizeController), JsonSerializer.Serialize(resourceSizeDto));
            
            var request = _mapper.Map<CreateResourceSizeCommand>(resourceSizeDto);
            
            _logger.LogInformation("{Class} {Method} {Json}", nameof(PostResourceSize), typeof(ResourceSizeController), JsonSerializer.Serialize(request));
            
            var dto = await _mediator.Send(request);

            _logger.LogInformation("{Class} {Method} {Json}", nameof(PostResourceSize), typeof(ResourceSizeController), JsonSerializer.Serialize(dto));
            
            return CreatedAtRoute(nameof(GetResourceSize), new { id = dto.Id }, dto);
        }
    }
}