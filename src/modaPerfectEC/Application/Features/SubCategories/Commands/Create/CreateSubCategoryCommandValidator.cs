using FluentValidation;

namespace Application.Features.SubCategories.Commands.Create;

public class CreateSubCategoryCommandValidator : AbstractValidator<CreateSubCategoryCommand>
{
    public CreateSubCategoryCommandValidator()
    {
        RuleFor(c => c.CategoryId).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
    }
}