using FluentValidation;

namespace Application.Features.ProductVariants.Commands.Create;

public class CreateProductVariantCommandValidator : AbstractValidator<CreateProductVariantCommand>
{
    public CreateProductVariantCommandValidator()
    {
        RuleFor(c => c.ProductId).NotEmpty();
        RuleFor(c => c.ColorDto.Color).NotEmpty();
        RuleFor(c => c.ColorDto.StockAmount).NotEmpty();
    }
}