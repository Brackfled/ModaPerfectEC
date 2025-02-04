using FluentValidation;

namespace Application.Features.Orders.Commands.Update;

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.BasketId).NotEmpty();
        RuleFor(c => c.OrderNo).NotEmpty();
        RuleFor(c => c.OrderPrice).NotEmpty();
        RuleFor(c => c.IsInvoiceSended).NotEmpty();
        RuleFor(c => c.OrderState).NotEmpty();
    }
}