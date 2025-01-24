using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProductVariants.Commands.UpdateStockAmount;
public class UpdatedStockAmountProductVariantResponse
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string Color { get; set; }
    public string Hex { get; set; }
    public int StockAmount { get; set; }
    public int[] Sizes { get; set; }
    public DateTime CreatedTime { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
}
