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
using SpaceTrading.Production.Domain.Features.ResourceSize.Update;

namespace SpaceTrading.Production.Api.Controllers
{
    [Route("api/[controller]")]
    public class ResourceSizeController : ApiController
    {
        private readonly IValidator<CreateResourceSizeCommandDto> _createCommandValidator;
        private readonly ILogger<ResourceSizeController> _logger;
        private readonly IValidator<PageParameters> _pageParametersValidator;
        private readonly IValidator<UpdateResourceSizeDto> _updateCommandValidator;

        public ResourceSizeController(
            IMapper mapper,
            IMediator mediator,
            IValidator<CreateResourceSizeCommandDto> createCommandValidator,
            IValidator<UpdateResourceSizeDto> updateCommandValidator,
            IValidator<PageParameters> pageParametersValidator,
            ILogger<ResourceSizeController> logger
        ) : base(mediator, mapper)
        {
            _createCommandValidator = createCommandValidator;
            _updateCommandValidator = updateCommandValidator;
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
        public async Task<IActionResult> CreateResourceSize(
            [FromBody] CreateResourceSizeCommandDto createResourceSizeCommandDto)
        {
            var validationResult = await _createCommandValidator.ValidateAsync(createResourceSizeCommandDto);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return BadRequest(ModelState);
            }

            var command = new CreateResourceSizeCommand(createResourceSizeCommandDto, GetCorrelationId());

            _logger.LogInformation("{Class} {Method} {Json}", nameof(CreateResourceSize),
                typeof(ResourceSizeController),
                JsonSerializer.Serialize(command));

            var resourceSizeDto = await _mediator.Send(command);

            _logger.LogInformation("{Class} {Method} {Json} {CorrelationId}", 
                typeof(ResourceSizeController),
                nameof(CreateResourceSize),
                JsonSerializer.Serialize(resourceSizeDto));

            return CreatedAtAction(nameof(GetResourceSize), new { resourceSizeDto.Id }, resourceSizeDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateResourceSize(int id, UpdateResourceSizeDto updateResourceSizeDto)
        {
            var correlationId = GetCorrelationId();
            
            _logger.LogInformation("{Class} {Method} {Json} {Id} {CorrelationId}", 
                typeof(ResourceSizeController),
                nameof(UpdateResourceSize),
                id.ToString(),
                JsonSerializer.Serialize(updateResourceSizeDto),
                correlationId.ToString());
            
            var validationResult = await _updateCommandValidator.ValidateAsync(updateResourceSizeDto);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return BadRequest(ModelState);
            }

            var command = new UpdateResourceSizeCommand(id, updateResourceSizeDto, GetCorrelationId());

            var resourceSizeDto = await _mediator.Send(command);

            return Ok(resourceSizeDto);
        }
    }
}