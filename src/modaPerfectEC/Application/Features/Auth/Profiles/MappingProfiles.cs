using Application.Features.Auth.Commands.RevokeToken;
using Application.Features.Auth.Commands.UserConfirm;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Auth.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<NArchitecture.Core.Security.Entities.RefreshToken<Guid, Guid>, RefreshToken>().ReverseMap();
        CreateMap<RefreshToken, RevokedTokenResponse>().ReverseMap();
        CreateMap<User, ConfirmedUserResponse>().ReverseMap();
    }
}
