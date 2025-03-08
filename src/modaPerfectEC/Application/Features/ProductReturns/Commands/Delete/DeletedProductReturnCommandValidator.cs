using FluentValidation;

namespace Application.Features.ProductReturns.Commands.Delete;

public class DeleteProductReturnCommandValidator : AbstractValidator<DeleteProductReturnCommand>
{
    public DeleteProductReturnCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}