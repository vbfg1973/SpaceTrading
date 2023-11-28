using FluentValidation;

namespace SpaceTrading.Production.Domain.Features.ResourceCategory.Create
{
    public class CreateResourceCategoryCommandDtoValidator : AbstractValidator<CreateResourceCategoryCommandDto>
    {
        public CreateResourceCategoryCommandDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Size)
                .GreaterThanOrEqualTo(1);
        }
    }
}