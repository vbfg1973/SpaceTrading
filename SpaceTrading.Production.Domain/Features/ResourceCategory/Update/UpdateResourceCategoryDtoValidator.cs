using FluentValidation;

namespace SpaceTrading.Production.Domain.Features.ResourceCategory.Update
{
    public class UpdateResourceCategoryDtoValidator : AbstractValidator<UpdateResourceCategoryDto>
    {
        public UpdateResourceCategoryDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull();
        }
    }
}