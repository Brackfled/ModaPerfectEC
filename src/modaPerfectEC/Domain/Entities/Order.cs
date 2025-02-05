using Domain.Enums;
using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Order : Entity<Guid>
{
    public Guid UserId { get; set; }
    public Guid BasketId { get; set; }
    public string? OrderNo { get; set; }
    public double OrderPrice { get; set; }
    public string? TrackingNumber { get; set; }
    public bool IsInvoiceSended { get; set; }
    public bool IsUsdPrice { get; set; }
    public OrderState OrderState { get; set; }

    public virtual Basket Basket { get; set; } = default!;
    public virtual User User { get; set; } = default!;

    public Order()
    {
        OrderNo = default!;
        TrackingNumber = default!;
    }

    public Order(Guid userId, Guid basketId, string? orderNo, double orderPrice, string? trackingNumber, bool ısInvoiceSended, bool ısUsdPrice, OrderState orderState, Basket basket, User user)
    {
        UserId = userId;
        BasketId = basketId;
        OrderNo = orderNo;
        OrderPrice = orderPrice;
        TrackingNumber = trackingNumber;
        IsInvoiceSended = ısInvoiceSended;
        IsUsdPrice = ısUsdPrice;
        OrderState = orderState;
        Basket = basket;
        User = user;
    }
}
