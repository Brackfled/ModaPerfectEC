using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Orders.Commands.Create;
public class CreateOrderRequest
{
    public Guid BasketId { get; set; }
    public double OrderPrice { get; set; }
    public bool IsUsdPrice { get; set; }
}
