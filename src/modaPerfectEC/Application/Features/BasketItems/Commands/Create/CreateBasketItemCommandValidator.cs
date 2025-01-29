using FluentValidation;

namespace Application.Features.BasketItems.Commands.Create;

public class CreateBasketItemCommandValidator : AbstractValidator<CreateBasketItemCommand>
{
    public CreateBasketItemCommandValidator()
    {        
        RuleFor(c => c.CreateBasketItemRequest.ProductAmount).NotEmpty();
    }
}