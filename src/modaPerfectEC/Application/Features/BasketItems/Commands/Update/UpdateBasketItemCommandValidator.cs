using FluentValidation;

namespace Application.Features.BasketItems.Commands.Update;

public class UpdateBasketItemCommandValidator : AbstractValidator<UpdateBasketItemCommand>
{
    public UpdateBasketItemCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.BasketId).NotEmpty();
        RuleFor(c => c.ProductAmount).NotEmpty();
        RuleFor(c => c.RemainingAfterDelivery).NotEmpty();
        RuleFor(c => c.IsReturned).NotEmpty();
    }
}