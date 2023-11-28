using FluentValidation;

namespace SpaceTrading.Production.Domain.Features
{
    public class PageParametersValidator : AbstractValidator<PageParameters>
    {
        public PageParametersValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThanOrEqualTo(1);

            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1);
        }
    }
}