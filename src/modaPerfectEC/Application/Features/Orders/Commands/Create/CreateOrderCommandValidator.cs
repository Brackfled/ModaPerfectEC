using FluentValidation;

namespace Application.Features.Orders.Commands.Create;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.BasketId).NotEmpty();
        RuleFor(c => c.OrderNo).NotEmpty();
        RuleFor(c => c.OrderPrice).NotEmpty();
        RuleFor(c => c.IsInvoiceSended).NotEmpty();
        RuleFor(c => c.OrderState).NotEmpty();
    }
}