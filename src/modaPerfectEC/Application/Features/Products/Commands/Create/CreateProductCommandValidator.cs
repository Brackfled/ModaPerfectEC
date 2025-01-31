using FluentValidation;

namespace Application.Features.Products.Commands.Create;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(c => c.CreateProductRequest.CategoryId).NotEmpty();
        RuleFor(c => c.CreateProductRequest.SubCategoryId).NotEmpty();
        RuleFor(c => c.CreateProductRequest.Name).NotEmpty();
        RuleFor(c => c.CreateProductRequest.Price).NotEmpty().GreaterThan(0);
        RuleFor(c => c.CreateProductRequest.Description).NotEmpty();
        RuleFor(c => c.CreateProductRequest.ProductState).IsInEnum();
    }
}