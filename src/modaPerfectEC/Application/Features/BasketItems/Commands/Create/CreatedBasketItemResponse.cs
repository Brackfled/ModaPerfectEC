using NArchitecture.Core.Application.Responses;

namespace Application.Features.BasketItems.Commands.Create;

public class CreatedBasketItemResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid BasketId { get; set; }
    public Guid? ProductId { get; set; }
    public Guid? ProductVariantId { get; set; }
    public int ProductAmount { get; set; }
    public int RemainingAfterDelivery { get; set; }
    public bool IsReturned { get; set; }
}