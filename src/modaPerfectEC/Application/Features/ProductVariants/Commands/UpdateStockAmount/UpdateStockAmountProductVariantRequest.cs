using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProductVariants.Commands.UpdateStockAmount;
public class UpdateStockAmountProductVariantRequest
{
    public Guid Id { get; set; }
    public bool Increase {  get; set; }
    public int ProcessAmount { get; set; }
}
