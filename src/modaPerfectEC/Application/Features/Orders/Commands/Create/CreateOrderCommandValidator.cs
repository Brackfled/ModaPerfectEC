using FluentValidation;

namespace Application.Features.Orders.Commands.Create;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.CreateOrderRequest.BasketId).NotEmpty();
        RuleFor(c => c.CreateOrderRequest.OrderPrice).NotEmpty();
        RuleFor(c => c.CreateOrderRequest.IsUsdPrice).NotNull();
    }
}