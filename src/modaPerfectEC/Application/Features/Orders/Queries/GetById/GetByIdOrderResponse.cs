using NArchitecture.Core.Application.Responses;
using Domain.Enums;

namespace Application.Features.Orders.Queries.GetById;

public class GetByIdOrderResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid BasketId { get; set; }
    public string OrderNo { get; set; }
    public double OrderPrice { get; set; }
    public string? TrackingNumber { get; set; }
    public bool IsInvoiceSended { get; set; }
    public OrderState OrderState { get; set; }
}