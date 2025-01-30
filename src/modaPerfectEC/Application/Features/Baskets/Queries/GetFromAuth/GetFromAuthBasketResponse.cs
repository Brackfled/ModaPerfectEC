using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Baskets.Queries.GetFromAuth;
public class GetFromAuthBasketResponse
{
    public Guid UserId { get; set; }
    public Guid Id { get; set; }
    public double TotalPrice { get; set; }
    public double TotalPriceUSD { get; set; }
    public bool IsOrderBasket { get; set; }
}
