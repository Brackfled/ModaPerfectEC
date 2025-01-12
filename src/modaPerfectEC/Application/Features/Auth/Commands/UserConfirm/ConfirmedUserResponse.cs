using Domain.Enums;
using NArchitecture.Core.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.UserConfirm;
public class ConfirmedUserResponse: IResponse
{
    public Guid Id { get; set; }
    public string TradeName { get; set; }
    public UserState UserState { get; set; }

    public ConfirmedUserResponse()
    {
        TradeName = string.Empty;
    }

    public ConfirmedUserResponse(Guid userId, string tradeName, UserState userState)
    {
        Id = userId;
        TradeName = tradeName;
        UserState = userState;
    }
}
