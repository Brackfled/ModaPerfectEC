using NArchitecture.Core.Application.Responses;
using Domain.Enums;

namespace Application.Features.Orders.Commands.Create;

public class CreatedOrderResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid BasketId { get; set; }
    public string OrderNo { get; set; }
    public double OrderPrice { get; set; }
    public string? TrackingNumber { get; set; }
    public bool IsInvoiceSended { get; set; }
    public OrderState OrderState { get; set; }
    public DateTime CreatedDate { get; set; }
}