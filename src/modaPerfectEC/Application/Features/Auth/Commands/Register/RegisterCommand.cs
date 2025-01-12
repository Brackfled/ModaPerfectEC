using Application.Features.Auth.Rules;
using Application.Services.AuthService;
using Application.Services.Repositories;
using Domain.Dtos;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Dtos;
using NArchitecture.Core.Security.Hashing;
using NArchitecture.Core.Security.JWT;

namespace Application.Features.Auth.Commands.Register;

public class RegisterCommand : IRequest<RegisteredResponse>
{
    public RegisterDto RegisterDto { get; set; }
    public string IpAddress { get; set; }

    public RegisterCommand()
    {
        RegisterDto = null;
        IpAddress = string.Empty;
    }

    public RegisterCommand(RegisterDto registerDto, string ıpAddress)
    {
        RegisterDto = registerDto;
        IpAddress = ıpAddress;
    }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisteredResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;
        private readonly AuthBusinessRules _authBusinessRules;

        public RegisterCommandHandler(
            IUserRepository userRepository,
            IAuthService authService,
            AuthBusinessRules authBusinessRules
        )
        {
            _userRepository = userRepository;
            _authService = authService;
            _authBusinessRules = authBusinessRules;
        }

        public async Task<RegisteredResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            await _authBusinessRules.UserEmailShouldBeNotExists(request.RegisterDto.Email);
            await _authBusinessRules.IdentityNumberIsAccurate(request.RegisterDto.IdentityNumber);
            await _authBusinessRules.UserIdentityShouldBeNotExists(request.RegisterDto.IdentityNumber);

            HashingHelper.CreatePasswordHash(
                request.RegisterDto.Password,
                passwordHash: out byte[] passwordHash,
                passwordSalt: out byte[] passwordSalt
            );

            HashingHelper.CreatePasswordHash(
                    request.RegisterDto.IdentityNumber,
                    passwordHash: out byte[] identityHash,
                    passwordSalt: out byte[] identitySalt
                );

            User newUser =
                new()
                {
                    Email = request.RegisterDto.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    IdentityNumberHash = identityHash,
                    IdentityNumberSalt = identitySalt,
                    TradeName = request.RegisterDto.TradeName,
                    FirstName = request.RegisterDto.FirstName,
                    LastName = request.RegisterDto.LastName,
                    Country = request.RegisterDto.Country,
                    City = request.RegisterDto.City,
                    District = request.RegisterDto.District,
                    Address = request.RegisterDto.Address,
                    GsmNumber = request.RegisterDto.GsmNumber,
                    TaxNumber = request.RegisterDto.TaxNumber,
                    TaxOffice = request.RegisterDto.TaxOffice,
                    Reference = request.RegisterDto.Reference,
                    CustomerCode = request.RegisterDto.CustomerCode,
                    CarrierCompanyInfo = request.RegisterDto.CarrierCompanyInfo,
                    UserState = Domain.Enums.UserState.Pending,
                };
            User createdUser = await _userRepository.AddAsync(newUser);


            Domain.Entities.RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(
                createdUser,
                request.IpAddress
            );
            Domain.Entities.RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);

            RegisteredResponse registeredResponse = new() { UserState = createdUser.UserState, RefreshToken = addedRefreshToken };
            return registeredResponse;
        }
    }
}
