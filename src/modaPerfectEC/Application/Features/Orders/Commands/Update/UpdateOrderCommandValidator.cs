using FluentValidation;

namespace Application.Features.Orders.Commands.Update;

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.OrderNo);
        RuleFor(c => c.OrderState).IsInEnum();
    }
}