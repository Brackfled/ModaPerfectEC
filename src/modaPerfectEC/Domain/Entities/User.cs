using Domain.Enums;

namespace Domain.Entities;

public class User : NArchitecture.Core.Security.Entities.User<Guid>
{
    public string TradeName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string? District { get; set; }
    public string Address { get; set; }
    public string GsmNumber { get; set; }
    public string? TaxOffice { get; set; }
    public string? TaxNumber { get; set; }
    public byte[]? IdentityNumberSalt { get; set; }
    public byte[]? IdentityNumberHash { get; set; }
    public string Reference { get; set; }
    public string? CustomerCode { get; set; }
    public string? CarrierCompanyInfo { get; set; }
    public UserState UserState { get; set; }

    public virtual ICollection<UserOperationClaim> UserOperationClaims { get; set; } = default!;
    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = default!;
    public virtual ICollection<OtpAuthenticator> OtpAuthenticators { get; set; } = default!;
    public virtual ICollection<EmailAuthenticator> EmailAuthenticators { get; set; } = default!;
    public virtual ICollection<Basket> Baskets { get; set; } = default!;
    public virtual ICollection<Order> Orders { get; set; } = default!;

    public User()
    {
        TradeName = string.Empty;
        FirstName = string.Empty;
        LastName = string.Empty;
        Country = string.Empty;
        City = string.Empty;
        District = null;
        Address = string.Empty;
        GsmNumber = string.Empty;
        TaxNumber = null;
        TaxOffice = null;
        Reference = string.Empty;
        CustomerCode = null;
        CarrierCompanyInfo = null;
    }

    public User(string tradeName, string firstName, string lastName, string country, string city, string district, string address, string gsmNumber, string? taxOffice, string? taxNumber, byte[]? ıdentityNumberSalt, byte[]? ıdentityNumberHash, string reference, string? customerCode, string? carrierCompanyInfo, UserState userState, ICollection<UserOperationClaim> userOperationClaims, ICollection<RefreshToken> refreshTokens, ICollection<OtpAuthenticator> otpAuthenticators, ICollection<EmailAuthenticator> emailAuthenticators, ICollection<Basket> baskets)
    {
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
        IdentityNumberSalt = ıdentityNumberSalt;
        IdentityNumberHash = ıdentityNumberHash;
        Reference = reference;
        CustomerCode = customerCode;
        CarrierCompanyInfo = carrierCompanyInfo;
        UserState = userState;
        UserOperationClaims = userOperationClaims;
        RefreshTokens = refreshTokens;
        OtpAuthenticators = otpAuthenticators;
        EmailAuthenticators = emailAuthenticators;
        Baskets = baskets;
    }
}
