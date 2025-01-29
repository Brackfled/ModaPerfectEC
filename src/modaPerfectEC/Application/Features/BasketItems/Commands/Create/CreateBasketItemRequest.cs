using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.BasketItems.Commands.Create;
public class CreateBasketItemRequest
{
    public Guid ProductId { get; set; }
    public Guid ProductVariantId { get; set; }
    public int ProductAmount { get; set; }
}
