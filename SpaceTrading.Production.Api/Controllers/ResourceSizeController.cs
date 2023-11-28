using System.Text.Json;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SpaceTrading.Production.Api.Validation;
using SpaceTrading.Production.Domain.Features.ResourceSize.Create;
using SpaceTrading.Production.Domain.Features.ResourceSize.Get;

namespace SpaceTrading.Production.Api.Controllers
{
    [Route("api/[controller]")]
    public class ResourceSizeController : ApiController
    {
        private readonly IValidator<CreateResourceSizeCommandDto> _createCommandValidator;
        private readonly ILogger<ResourceSizeController> _logger;

        public ResourceSizeController(
            IMapper mapper,
            IMediator mediator,
            IValidator<CreateResourceSizeCommandDto> createCommandValidator,
            ILogger<ResourceSizeController> logger
        ) : base(mediator, mapper)
        {
            _createCommandValidator = createCommandValidator;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetResourceSize(int id)
        {
            var dto = await _mediator.Send(new GetResourceSizeById(id));
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> PostResourceSize(CreateResourceSizeCommandDto createResourceSizeCommandDto)
        {
            var validationResult = await _createCommandValidator.ValidateAsync(createResourceSizeCommandDto);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return BadRequest(ModelState);
            }

            var command = new CreateResourceSizeCommand(createResourceSizeCommandDto, GetCorrelationId());
            
            _logger.LogInformation("{Class} {Method} {Json}", nameof(PostResourceSize),
                typeof(ResourceSizeController),
                JsonSerializer.Serialize(command));

            var resourceSizeDto = await _mediator.Send(command);

            _logger.LogInformation("{Class} {Method} {Json} {CorrelationId}", nameof(PostResourceSize),
                typeof(ResourceSizeController),
                JsonSerializer.Serialize(resourceSizeDto));

            return CreatedAtAction(nameof(GetResourceSize), new { resourceSizeDto.Id }, resourceSizeDto);
        }
    }
}