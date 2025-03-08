using FluentValidation;

namespace Application.Features.ProductVariants.Commands.Update;

public class UpdateProductVariantCommandValidator : AbstractValidator<UpdateProductVariantCommand>
{
    public UpdateProductVariantCommandValidator()
    {
        RuleFor(c => c.UpdateProductVariantRequest.Id).NotEmpty();
        RuleFor(c => c.UpdateProductVariantRequest.Color);
        RuleFor(c => c.UpdateProductVariantRequest.Hex);
        RuleFor(c => c.UpdateProductVariantRequest.StockAmount).GreaterThan(0);
        RuleFor(c => c.UpdateProductVariantRequest.Sizes);
    }
}