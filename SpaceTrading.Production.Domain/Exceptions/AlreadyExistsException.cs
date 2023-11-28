using SpaceTrading.Production.Domain.Exceptions.Abstract;

namespace SpaceTrading.Production.Domain.Exceptions
{
    public class AlreadyExistsException : DomainException
    {
        public AlreadyExistsException(Type modelType, string identifyingData) : base(
            $"{modelType.Name} already exists ({identifyingData})")
        {
        }
    }
}