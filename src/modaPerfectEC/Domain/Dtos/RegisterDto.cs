using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos;
public class RegisterDto
{
    public string Email { get; set; }
    public string Password { get; set; }
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
    public string IdentityNumber { get; set; }
    public string Reference { get; set; }
    public string? CustomerCode { get; set; }
    public string? CarrierCompanyInfo { get; set; }
}
