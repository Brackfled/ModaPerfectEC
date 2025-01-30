using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Basket: Entity<Guid>
{   
    public Guid UserId { get; set; }
    public double TotalPrice { get; set; }
    public double TotalPriceUSD { get; set; }
    public bool IsOrderBasket { get; set; }

    public virtual User? User { get; set; }
    public ICollection<BasketItem>? BasketItems { get; set; }

    public Basket()
    {
        TotalPrice = 0;
        TotalPriceUSD = 0;
        IsOrderBasket = false;
    }

    public Basket(Guid userId, double totalPrice, double totalPriceUSD, bool ısOrderBasket, User? user, ICollection<BasketItem>? basketItems)
    {
        UserId = userId;
        TotalPrice = totalPrice;
        TotalPriceUSD = totalPriceUSD;
        IsOrderBasket = ısOrderBasket;
        User = user;
        BasketItems = basketItems;
    }
}
