using System.Text.RegularExpressions;
using FluentValidation;

namespace Application.Features.Users.Commands.UpdateFromAuth;

public class UpdateUserFromAuthCommandValidator : AbstractValidator<UpdateUserFromAuthCommand>
{
    public UpdateUserFromAuthCommandValidator()
    {
        RuleFor(c => c.UserUpdateFromAuthRequestDto.TradeName);
        RuleFor(c => c.UserUpdateFromAuthRequestDto.FirstName);
        RuleFor(c => c.UserUpdateFromAuthRequestDto.LastName);
        RuleFor(c => c.UserUpdateFromAuthRequestDto.Country);
        RuleFor(c => c.UserUpdateFromAuthRequestDto.City);
        RuleFor(c => c.UserUpdateFromAuthRequestDto.District);
        RuleFor(c => c.UserUpdateFromAuthRequestDto.Address);
        RuleFor(c => c.UserUpdateFromAuthRequestDto.GsmNumber);
        RuleFor(c => c.UserUpdateFromAuthRequestDto.TaxNumber);
        RuleFor(c => c.UserUpdateFromAuthRequestDto.TaxOffice);
        RuleFor(c => c.UserUpdateFromAuthRequestDto.Reference);

    }
}
