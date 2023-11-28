using System.Text.Json;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SpaceTrading.Production.Api.Validation;
using SpaceTrading.Production.Domain.Features;
using SpaceTrading.Production.Domain.Features.ResourceCategory.Create;
using SpaceTrading.Production.Domain.Features.ResourceCategory.GetAll;
using SpaceTrading.Production.Domain.Features.ResourceCategory.GetById;
using SpaceTrading.Production.Domain.Features.ResourceCategory.Update;

namespace SpaceTrading.Production.Api.Controllers
{
    [Route("api/[controller]")]
    public class ResourceCategoryController : ApiController
    {
        private readonly IValidator<CreateResourceCategoryCommandDto> _createCommandValidator;
        private readonly ILogger<ResourceCategoryController> _logger;
        private readonly IValidator<PageParameters> _pageParametersValidator;
        private readonly IValidator<UpdateResourceCategoryDto> _updateCommandValidator;

        public ResourceCategoryController(
            IMapper mapper,
            IMediator mediator,
            IValidator<CreateResourceCategoryCommandDto> createCommandValidator,
            IValidator<UpdateResourceCategoryDto> updateCommandValidator,
            IValidator<PageParameters> pageParametersValidator,
            ILogger<ResourceCategoryController> logger
        ) : base(mediator, mapper)
        {
            _createCommandValidator = createCommandValidator;
            _updateCommandValidator = updateCommandValidator;
            _pageParametersValidator = pageParametersValidator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> PageAllResourceCategories([FromQuery] PageParameters pageParameters)
        {
            var validationResult = await _pageParametersValidator.ValidateAsync(pageParameters);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return BadRequest(ModelState);
            }

            var query = new PageAllResourceCategory(pageParameters, GetCorrelationId());

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetResourceCategory(int id)
        {
            var dto = await _mediator.Send(new GetResourceCategoryById(id, GetCorrelationId()));
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateResourceCategory(
            [FromBody] CreateResourceCategoryCommandDto createResourceCategoryCommandDto)
        {
            
            var validationResult = await _createCommandValidator.ValidateAsync(createResourceCategoryCommandDto);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return BadRequest(ModelState);
            }

            var command = new CreateResourceCategoryCommand(createResourceCategoryCommandDto, GetCorrelationId());

            var ResourceCategoryDto = await _mediator.Send(command);

            _logger.LogInformation("{Class} {Method} {Json} {CorrelationId}", 
                typeof(ResourceCategoryController),
                nameof(CreateResourceCategory),
                JsonSerializer.Serialize(ResourceCategoryDto),
                command.CorrelationId.ToString());

            return CreatedAtAction(nameof(GetResourceCategory), new { ResourceCategoryDto.Id }, ResourceCategoryDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateResourceCategory(int id, UpdateResourceCategoryDto updateResourceCategoryDto)
        {
            var correlationId = GetCorrelationId();
            
            _logger.LogInformation("{Class} {Method} {Json} {Id} {CorrelationId}", 
                typeof(ResourceCategoryController),
                nameof(UpdateResourceCategory),
                id.ToString(),
                JsonSerializer.Serialize(updateResourceCategoryDto),
                correlationId.ToString());
            
            var validationResult = await _updateCommandValidator.ValidateAsync(updateResourceCategoryDto);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return BadRequest(ModelState);
            }

            var command = new UpdateResourceCategoryCommand(id, updateResourceCategoryDto, GetCorrelationId());

            var resourceCategoryDto = await _mediator.Send(command);

            return Ok(resourceCategoryDto);
        }
    }
}