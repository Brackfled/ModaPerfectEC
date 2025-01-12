using Domain.Enums;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Security.JWT;

namespace Application.Features.Auth.Commands.Register;

public class RegisteredResponse : IResponse
{
    public UserState UserState { get; set; }
    public Domain.Entities.RefreshToken RefreshToken { get; set; }

    public RegisteredResponse()
    {
        RefreshToken = null!;
    }

    public RegisteredResponse(UserState userState, Domain.Entities.RefreshToken refreshToken)
    {
        UserState = userState;
        RefreshToken = refreshToken;
    }
}
