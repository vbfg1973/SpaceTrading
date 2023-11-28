using System.Reflection;

namespace SpaceTrading.Production.Domain
{
    public sealed class ProductionDomainAssemblyReference
    {
        public static readonly Assembly Assembly = typeof(ProductionDomainAssemblyReference).Assembly;
    }
}