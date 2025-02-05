using NArchitecture.Core.Application.Responses;
using Domain.Enums;
using Domain.Entities;

namespace Application.Features.Orders.Queries.GetById;

public class GetByIdOrderResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string UserEmail { get; set; }
    public string UserTradeName { get; set; }
    public string UserFirstName { get; set; }
    public string UserLastName { get; set; }
    public string UserCountry { get; set; }
    public string UserCity { get; set; }
    public string? UserDistrict { get; set; }
    public string UserAddress { get; set; }
    public string UserGsmNumber { get; set; }
    public string? UserTaxOffice { get; set; }
    public string? UserTaxNumber { get; set; }
    public string UserReference { get; set; }
    public string? UserCustomerCode { get; set; }
    public string? UserCarrierCompanyInfo { get; set; }
    public Guid BasketId { get; set; }
    public double BasketTotalPrice { get; set; }
    public double BasketTotalPriceUSD { get; set; }
    public string? OrderNo { get; set; }
    public double OrderPrice { get; set; }
    public string? TrackingNumber { get; set; }
    public bool IsInvoiceSended { get; set; }
    public bool IsUsdPrice { get; set; }
    public OrderState OrderState { get; set; }

    public ICollection<BasketItem> BasketItems { get; set; }

}