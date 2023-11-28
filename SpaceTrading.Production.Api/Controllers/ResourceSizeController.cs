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
        public async Task<IActionResult> PostResourceSize(CreateResourceSizeDto createResourceSizeDto)
        {
            _logger.LogInformation("{Class} {Method} {Json}", nameof(PostResourceSize), typeof(ResourceSizeController),
                JsonSerializer.Serialize(createResourceSizeDto));

            var createResourceSizeCommand = _mapper.Map<CreateResourceSizeCommand>(createResourceSizeDto);

            _logger.LogInformation("{Class} {Method} {Json}", nameof(PostResourceSize), typeof(ResourceSizeController),
                JsonSerializer.Serialize(createResourceSizeCommand));

            var resourceSizeDto = await _mediator.Send(createResourceSizeCommand);

            _logger.LogInformation("{Class} {Method} {Json}", nameof(PostResourceSize), typeof(ResourceSizeController),
                JsonSerializer.Serialize(resourceSizeDto));

            return CreatedAtAction(nameof(GetResourceSize), new { resourceSizeDto.Id }, resourceSizeDto);
        }
    }
}