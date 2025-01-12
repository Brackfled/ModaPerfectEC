using Domain.Enums;
using NArchitecture.Core.Application.Responses;

namespace Application.Features.Users.Commands.Create;

public class CreatedUserResponse : IResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public UserState UserState { get; set; }
    public DateTime CreatedDate { get; set; }

    public CreatedUserResponse()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        Email = string.Empty;
    }

    public CreatedUserResponse(Guid ýd, string firstName, string lastName, string email, UserState userState)
    {
        Id = ýd;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        UserState = userState;
    }
}
