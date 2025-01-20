using FluentValidation;

namespace Application.Features.ProductVariants.Commands.Delete;

public class DeleteProductVariantCommandValidator : AbstractValidator<DeleteProductVariantCommand>
{
    public DeleteProductVariantCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}