namespace SpaceTrading.Production.Domain.Exceptions.Abstract
{
    public abstract class DomainException : Exception
    {
        protected DomainException(string message) : base(message)
        {
        }
    }
}