using Domain.Enums;
using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Users.Queries.GetList;

public class GetListUserListItemDto : IDto
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string TradeName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string District { get; set; }
    public string Address { get; set; }
    public string GsmNumber { get; set; }
    public string? TaxOffice { get; set; }
    public string? TaxNumber { get; set; }
    public string Reference { get; set; }
    public string? CustomerCode { get; set; }
    public string? CarrierCompanyInfo { get; set; }
    public UserState UserState { get; set; }

    public GetListUserListItemDto()
    {
        Email = string.Empty;
        TradeName = string.Empty;
        FirstName = string.Empty;
        LastName = string.Empty;
        Country = string.Empty;
        City = string.Empty;
        District = string.Empty;
        Address = string.Empty;
        GsmNumber = string.Empty;
        TaxNumber = null;
        TaxOffice = null;
        Reference = string.Empty;
        CustomerCode = null;
        CarrierCompanyInfo = null;
    }

    public GetListUserListItemDto(Guid ýd, string email, string tradeName, string firstName, string lastName, string country, string city, string district, string address, string gsmNumber, string? taxOffice, string? taxNumber, string reference, string? customerCode, string? carrierCompanyInfo, UserState userState)
    {
        Id = ýd;
        Email = email;
        TradeName = tradeName;
        FirstName = firstName;
        LastName = lastName;
        Country = country;
        City = city;
        District = district;
        Address = address;
        GsmNumber = gsmNumber;
        TaxOffice = taxOffice;
        TaxNumber = taxNumber;
        Reference = reference;
        CustomerCode = customerCode;
        CarrierCompanyInfo = carrierCompanyInfo;
        UserState = userState;
    }
}
