using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities;
public class ProductVariant: Entity<Guid>
{
    public Guid ProductId { get; set; }
    public string Color { get; set; }
    public string Hex {  get; set; }
    public int StockAmount { get; set; }
    public int[] Sizes { get; set; }

    [JsonIgnore]
    public virtual Product? Product { get; set; }
    public virtual ICollection<BasketItem> BasketItems { get; set; } = default!;

    public ProductVariant()
    {
        Color = string.Empty;
        Hex = string.Empty;
        Sizes = Array.Empty<int>();
    }

    public ProductVariant(Guid productId, string color, string hex, int stockAmount, int[] sizes, Product? product, ICollection<BasketItem> basketItems)
    {
        ProductId = productId;
        Color = color;
        Hex = hex;
        StockAmount = stockAmount;
        Sizes = sizes;
        Product = product;
        BasketItems = basketItems;
    }
}
