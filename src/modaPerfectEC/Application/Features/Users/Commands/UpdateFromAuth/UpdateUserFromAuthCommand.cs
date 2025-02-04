using Application.Features.Users.Rules;
using Application.Services.AuthService;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Transaction;
using NArchitecture.Core.Security.Hashing;

namespace Application.Features.Users.Commands.UpdateFromAuth;

public class UpdateUserFromAuthCommand : IRequest<UpdatedUserFromAuthResponse>,ITransactionalRequest
{
    public Guid Id { get; set; }
    public UserUpdateFromAuthRequestDto UserUpdateFromAuthRequestDto { get; set; }

    public UpdateUserFromAuthCommand()
    {
        UserUpdateFromAuthRequestDto = null!;
    }
    public UpdateUserFromAuthCommand(Guid id, UserUpdateFromAuthRequestDto userUpdateFromAuthRequestDto)
    {
        Id = id;
        UserUpdateFromAuthRequestDto = userUpdateFromAuthRequestDto;
    }

    public class UpdateUserFromAuthCommandHandler : IRequestHandler<UpdateUserFromAuthCommand, UpdatedUserFromAuthResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly UserBusinessRules _userBusinessRules;
        private readonly IAuthService _authService;

        public UpdateUserFromAuthCommandHandler(
            IUserRepository userRepository,
            IMapper mapper,
            UserBusinessRules userBusinessRules,
            IAuthService authService
        )
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userBusinessRules = userBusinessRules;
            _authService = authService;
        }

        public async Task<UpdatedUserFromAuthResponse> Handle(
            UpdateUserFromAuthCommand request,
            CancellationToken cancellationToken
        )
        {
            User? user = await _userRepository.GetAsync(
                predicate: u => u.Id.Equals(request.Id),
                cancellationToken: cancellationToken
            );
            await _userBusinessRules.UserShouldBeExistsWhenSelected(user);

            user = _mapper.Map(request.UserUpdateFromAuthRequestDto, user);
            user.UserState = Domain.Enums.UserState.Pending;

            User updatedUser = await _userRepository.UpdateAsync(user!);

            UpdatedUserFromAuthResponse response = _mapper.Map<UpdatedUserFromAuthResponse>(updatedUser);
            return response;
        }
    }
}
