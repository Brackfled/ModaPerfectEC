using FluentValidation;

namespace Application.Features.ProductReturns.Commands.Create;

public class CreateProductReturnCommandValidator : AbstractValidator<CreateProductReturnCommand>
{
    public CreateProductReturnCommandValidator()
    {
        RuleFor(c => c.BasketItemId).NotEmpty();
        RuleFor(c => c.OrderId).NotEmpty();
        RuleFor(c => c.Description).NotEmpty();
        RuleFor(c => c.ReturnState).NotEmpty();
    }
}