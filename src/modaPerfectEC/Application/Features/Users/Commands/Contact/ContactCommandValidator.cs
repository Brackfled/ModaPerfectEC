using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands.Contact;
public class ContactCommandValidator: AbstractValidator<ContactCommand>
{
    public ContactCommandValidator()
    {
        RuleFor(c => c.Email).NotNull().EmailAddress();
        RuleFor(c => c.Name).NotNull().NotEmpty().MaximumLength(50);
        RuleFor(c => c.GsmNumber).NotNull().NotEmpty();
        RuleFor(c => c.Subject).NotEmpty().NotNull().MaximumLength(50);
        RuleFor(c => c.Message).NotNull().NotEmpty().MaximumLength(250);
    }
}
