using MediatR;

namespace SpaceTrading.Production.Domain.Features
{
    public interface ICommand<out T> : IRequest<T>
    {
        public Guid CorrelationId { get; }
    }
}