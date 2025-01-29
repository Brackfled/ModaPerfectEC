using NArchitecture.Core.Application.Dtos;

namespace Application.Features.BasketItems.Queries.GetList;

public class GetListBasketItemListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid BasketId { get; set; }
    public Guid? ProductId { get; set; }
    public Guid? ProductVariantId { get; set; }
    public int ProductAmount { get; set; }
    public int RemainingAfterDelivery { get; set; }
    public bool IsReturned { get; set; }
}