using FluentValidation;

namespace SpaceTrading.Production.Domain.Features.ResourceSize.Create
{
    public class CreateResourceSizeCommandDtoValidator : AbstractValidator<CreateResourceSizeCommandDto>
    {
        public CreateResourceSizeCommandDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Size)
                .GreaterThanOrEqualTo(1);
        }
    }
}