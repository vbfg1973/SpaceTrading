using FluentValidation;

namespace SpaceTrading.Production.Domain.Features.ResourceSize.Update
{
    public class UpdateResourceSizeDtoValidator : AbstractValidator<UpdateResourceSizeDto>
    {
        public UpdateResourceSizeDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Size)
                .GreaterThanOrEqualTo(1);
        }
    }
}