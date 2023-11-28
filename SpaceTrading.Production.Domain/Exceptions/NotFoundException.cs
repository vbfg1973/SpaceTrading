using SpaceTrading.Production.Domain.Exceptions.Abstract;

namespace SpaceTrading.Production.Domain.Exceptions
{
    public class NotFoundException : DomainException
    {
        public NotFoundException(Type modelType, int id) : base($"{modelType.Name} not found with id {id}")
        {
        }
    }
}