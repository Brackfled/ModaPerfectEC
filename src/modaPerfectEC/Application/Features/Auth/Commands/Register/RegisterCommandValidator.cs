using System.Text.RegularExpressions;
using FluentValidation;

namespace Application.Features.Auth.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(c => c.RegisterDto.Email).NotEmpty().EmailAddress();
        RuleFor(c => c.RegisterDto.Password)
            .NotEmpty()
            .MinimumLength(8)
            .Must(StrongPassword)
            .WithMessage(
                "Password must contain at least one uppercase letter, one lowercase letter, one number and one special character."
            );
        RuleFor(c => c.RegisterDto.TradeName).NotEmpty();
        RuleFor(c => c.RegisterDto.FirstName).NotEmpty();
        RuleFor(c => c.RegisterDto.LastName).NotEmpty();
        RuleFor(c => c.RegisterDto.Country).NotEmpty();
        RuleFor(c => c.RegisterDto.City).NotEmpty();
        RuleFor(c => c.RegisterDto.District).NotEmpty();
        RuleFor(c => c.RegisterDto.Address).NotEmpty();
        RuleFor(c => c.RegisterDto.GsmNumber).NotEmpty();
        RuleFor(c => c.RegisterDto.TaxNumber);
        RuleFor(c => c.RegisterDto.TaxOffice);
        RuleFor(c => c.RegisterDto.Reference).NotEmpty();
        RuleFor(c => c.RegisterDto.CustomerCode).MinimumLength(6);
    }

    private bool StrongPassword(string value)
    {
        Regex strongPasswordRegex = new("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", RegexOptions.Compiled);

        return strongPasswordRegex.IsMatch(value);
    }
}
