using FluentValidation;

namespace SpaceTrading.Production.Domain.Features.ResourceSize.Create
{
    public class CreateResourceSizeCommandValidator : AbstractValidator<CreateResourceSizeCommand>
    {
        public CreateResourceSizeCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Size)
                .GreaterThanOrEqualTo(1);
        }
    }
}