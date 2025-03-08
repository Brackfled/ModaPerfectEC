using Domain.Enums;
using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class ProductReturn: Entity<Guid>
{
    public Guid BasketItemId { get; set; }
    public Guid OrderId { get; set; }
    public string Description { get; set; }
    public ReturnState ReturnState { get; set; }

    public virtual BasketItem BasketItem { get; set; } = default!;
    public virtual Order Order { get; set; } = default!;
    public virtual ICollection<ReturnImage> ReturnImages { get; set; } = default!;

    public ProductReturn()
    {
        Description = string.Empty;
    }

    public ProductReturn(Guid basketItemId, Guid orderId, string description, ReturnState returnState, BasketItem basketItem, Order order, ICollection<ReturnImage> returnImages)
    {
        BasketItemId = basketItemId;
        OrderId = orderId;
        Description = description;
        ReturnState = returnState;
        BasketItem = basketItem;
        Order = order;
        ReturnImages = returnImages;
    }
}
