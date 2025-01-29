using FluentValidation;

namespace Application.Features.Baskets.Commands.Create;

public class CreateBasketCommandValidator : AbstractValidator<CreateBasketCommand>
{
    public CreateBasketCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.TotalPrice).NotEmpty();
        RuleFor(c => c.IsOrderBasket).NotEmpty();
    }
}