using System.Text.Json;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SpaceTrading.Production.Api.Validation;
using SpaceTrading.Production.Domain.Features;
using SpaceTrading.Production.Domain.Features.ResourceSize.Create;
using SpaceTrading.Production.Domain.Features.ResourceSize.GetAll;
using SpaceTrading.Production.Domain.Features.ResourceSize.GetById;

namespace SpaceTrading.Production.Api.Controllers
{
    [Route("api/[controller]")]
    public class ResourceSizeController : ApiController
    {
        private readonly IValidator<CreateResourceSizeCommandDto> _createCommandValidator;
        private readonly IValidator<PageParameters> _pageParametersValidator;
        private readonly ILogger<ResourceSizeController> _logger;

        public ResourceSizeController(
            IMapper mapper,
            IMediator mediator,
            IValidator<CreateResourceSizeCommandDto> createCommandValidator,
            IValidator<PageParameters> pageParametersValidator,
            ILogger<ResourceSizeController> logger
        ) : base(mediator, mapper)
        {
            _createCommandValidator = createCommandValidator;
            _pageParametersValidator = pageParametersValidator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> PageAllResourceSizes([FromQuery] PageParameters pageParameters)
        {
            var validationResult = await _pageParametersValidator.ValidateAsync(pageParameters);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return BadRequest(ModelState);
            }

            var query = new PageAllResourceSize(pageParameters, GetCorrelationId());

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetResourceSize(int id)
        {
            var dto = await _mediator.Send(new GetResourceSizeById(id, GetCorrelationId()));
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> PostResourceSize([FromBody] CreateResourceSizeCommandDto createResourceSizeCommandDto)
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