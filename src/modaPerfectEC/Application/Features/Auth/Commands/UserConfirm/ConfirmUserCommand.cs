using Application.Features.Auth.Constants;
using Application.Features.Users.Rules;
using Application.Services.UsersService;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.UserConfirm;
public class ConfirmUserCommand: IRequest<ConfirmedUserResponse>, ISecuredRequest
{
    public Guid UserId { get; set; }
    public UserState UserState { get; set; }

    public string[] Roles => [AuthOperationClaims.UpdateUserState];

    public class ConfirmUserCommandHandler: IRequestHandler<ConfirmUserCommand, ConfirmedUserResponse>
    {
        private readonly IUserService _userService;
        private readonly UserBusinessRules _userBusinessRules;
        private IMapper _mapper;

        public ConfirmUserCommandHandler(IUserService userService, UserBusinessRules userBusinessRules, IMapper mapper)
        {
            _userService = userService;
            _userBusinessRules = userBusinessRules;
            _mapper = mapper;
        }

        public async Task<ConfirmedUserResponse> Handle(ConfirmUserCommand request, CancellationToken cancellationToken)
        {
            await _userBusinessRules.UserIdShouldBeExistsWhenSelected(request.UserId);

            User? user = await _userService.GetAsync(u => u.Id == request.UserId);

            await _userBusinessRules.UserStateIsAccurate(user, request.UserState);

            user.UserState = request.UserState;

            User updatedUser = await _userService.UpdateAsync(user);

            ConfirmedUserResponse response = _mapper.Map<ConfirmedUserResponse>(updatedUser);
            return response;
        }
    }
}
