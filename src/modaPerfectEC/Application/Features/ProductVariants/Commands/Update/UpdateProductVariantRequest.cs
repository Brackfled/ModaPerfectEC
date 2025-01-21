using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProductVariants.Commands.Update;
public class UpdateProductVariantRequest
{
    public Guid Id { get; set; }
    public string? Color { get; set; }
    public string? Hex { get; set; }
    public int? StockAmount { get; set; }
    public int[]? Sizes { get; set; }
}
