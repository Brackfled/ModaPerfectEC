using Application.Features.Orders.Queries.GetListAll;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Orders.Queries.GetListFromAuth;
public class GetListFromAuthOrderListItemDto
{
    public Guid Id { get; set; }
    public string? OrderNo { get; set; }
    public double OrderPrice { get; set; }
    public string? TrackingNumber { get; set; }
    public bool IsInvoiceSended { get; set; }
    public bool IsUsdPrice { get; set; }
    public OrderState OrderState { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public DateTime? DeleteDDate { get; set; }
    public ICollection<BasketItemDto> BasketItems { get; set; }
}
