using NArchitecture.Core.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.BasketItems.Queries.GetFromAuth;
public class GetByBasketIdBasketItemListItemDto: IResponse
{
    public Guid Id { get; set; }
    public Guid BasketId { get; set; }
    public Guid BasketUserId { get; set; }
    public Guid? ProductId { get; set; }
    public double ProductPrice { get; set; }
    public string ProductName { get; set; }
    public Guid? ProductVariantId { get; set; }
    public int ProductAmount { get; set; }
    public int RemainingAfterDelivery { get; set; }
    public bool IsReturned { get; set; }

    public double BasketItemTotalPrice => Math.Round(ProductPrice * ProductAmount, 2, MidpointRounding.AwayFromZero);

}
