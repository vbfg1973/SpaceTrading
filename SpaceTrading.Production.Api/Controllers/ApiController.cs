using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SpaceTrading.Production.Api.Controllers
{

    [ApiController]
    [Produces("application/json")]
    [ApiVersion("1.0")]
    public abstract class ApiController : ControllerBase
    {
        protected readonly IMediator _mediator;
        private readonly IMapper _mapper;

        protected ApiController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        private protected Guid GetCorrelationId()
        {
            var correlationId = HttpContext?.Request?.Headers["X-Correlation-ID"].FirstOrDefault();
            if (string.IsNullOrEmpty(correlationId))
            {
                correlationId = Guid.NewGuid().ToString();
            }
        
            return Guid.Parse(correlationId);
        } 
    }
}