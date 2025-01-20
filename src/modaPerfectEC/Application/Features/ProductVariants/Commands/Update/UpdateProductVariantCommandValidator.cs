using FluentValidation;

namespace Application.Features.ProductVariants.Commands.Update;

public class UpdateProductVariantCommandValidator : AbstractValidator<UpdateProductVariantCommand>
{
    public UpdateProductVariantCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.ProductId).NotEmpty();
        RuleFor(c => c.Color).NotEmpty();
        RuleFor(c => c.StockAmount).NotEmpty();
    }
}