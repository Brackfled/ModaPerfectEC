using FluentValidation;

namespace Application.Features.ProductReturns.Commands.Update;

public class UpdateProductReturnCommandValidator : AbstractValidator<UpdateProductReturnCommand>
{
    public UpdateProductReturnCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.BasketItemId).NotEmpty();
        RuleFor(c => c.OrderId).NotEmpty();
        RuleFor(c => c.Description).NotEmpty();
        RuleFor(c => c.ReturnState).NotEmpty();
    }
}