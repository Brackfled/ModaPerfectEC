using FluentValidation;

namespace Application.Features.Products.Commands.Update;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(c => c.Id);
        RuleFor(c => c.UpdateProductRequest.CategoryId);
        RuleFor(c => c.UpdateProductRequest.SubCategoryId);
        RuleFor(c => c.UpdateProductRequest.Name);
        RuleFor(c => c.UpdateProductRequest.Price);
        RuleFor(c => c.UpdateProductRequest.Description);
        RuleFor(c => c.UpdateProductRequest.ProductState);
    }
}