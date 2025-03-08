using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProductVariants.Commands.UpdateStockAmount;
public class UpdateStockAmountProductVariantCommandValidator: AbstractValidator<UpdateStockAmountProductVariantCommand>
{
    public UpdateStockAmountProductVariantCommandValidator()
    {
        RuleFor(c => c.UpdateStockAmountProductVariantRequest.ProcessAmount).GreaterThan(0);
    }
}
