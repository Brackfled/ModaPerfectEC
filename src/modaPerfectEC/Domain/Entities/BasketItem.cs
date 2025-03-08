using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class BasketItem: Entity<Guid>
{
    public Guid BasketId { get; set; }
    public Guid? ProductId { get; set; }
    public Guid? ProductVariantId { get; set; }
    public int ProductAmount { get; set; }
    public int RemainingAfterDelivery { get; set; }
    public bool IsReturned { get; set; }

    public virtual Basket? Basket { get; set; }
    public virtual Product? Product { get; set; }
    public virtual ProductVariant? ProductVariant { get; set; }
    public virtual ICollection<ProductReturn> ProductReturns { get; set; } = default!;

    public BasketItem()
    {
        IsReturned = false;
    }

    public BasketItem(Guid basketId, Guid? productId, Guid? productVariantId, int productAmount, int remainingAfterDelivery, bool ısReturned, Basket? basket, Product? product, ProductVariant? productVariant)
    {
        BasketId = basketId;
        ProductId = productId;
        ProductVariantId = productVariantId;
        ProductAmount = productAmount;
        RemainingAfterDelivery = remainingAfterDelivery;
        IsReturned = ısReturned;
        Basket = basket;
        Product = product;
        ProductVariant = productVariant;
    }
}
