using Amazon.Runtime.Internal;
using Application.Features.Auth.Commands.UserConfirm;
using Application.Features.UserOperationClaims.Rules;
using Application.Features.Users.Rules;
using Application.Services.Repositories;
using Application.Services.UserOperationClaims;
using Application.Services.UsersService;
using Domain.Dtos;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Transaction;
using NArchitecture.Core.Security.Constants;
using NArchitecture.Core.Security.Hashing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Commands.ConfirmAdmin;
public class ConfirmAdminUserOperationClaimCommand: IRequest<ConfirmAdminUserOperationClaimResponse>, ITransactionalRequest, ISecuredRequest
{
    public Guid UserId { get; set; }
    public string SecretKey { get; set; }

    public string[] Roles => [GeneralOperationClaims.Admin];

    public class ConfirmAdminUserOperationClaimCommandHandler : IRequestHandler<ConfirmAdminUserOperationClaimCommand, ConfirmAdminUserOperationClaimResponse>
    {
        private readonly IUserService _userService;
        private readonly IUserOperationClaimService _userOperationClaimService;
        private readonly UserBusinessRules _userBusinessRules;
        private readonly UserOperationClaimBusinessRules _userOperationClaimBusinessRules;

        public ConfirmAdminUserOperationClaimCommandHandler(IUserService userService, IUserOperationClaimService userOperationClaimService, UserBusinessRules userBusinessRules, UserOperationClaimBusinessRules userOperationClaimBusinessRules)
        {
            _userService = userService;
            _userOperationClaimService = userOperationClaimService;
            _userBusinessRules = userBusinessRules;
            _userOperationClaimBusinessRules = userOperationClaimBusinessRules;
        }

        public async Task<ConfirmAdminUserOperationClaimResponse> Handle(ConfirmAdminUserOperationClaimCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userService.GetAsync(u => u.Id == request.UserId);
            await _userBusinessRules.UserShouldBeExistsWhenSelected(user);

            ConfirmAdminUserOperationClaimResponse response = new();
            response.UserId = user!.Id;

            string[] _keys  = request.SecretKey.Split("+++", StringSplitOptions.None);

            bool isVerified = HashingHelper.VerifyPasswordHash(_keys[1], user!.IdentityNumberHash!, user!.IdentityNumberSalt!);

            if(user.FirstName == _keys[0] && user.LastName == _keys[2] && isVerified)
            {
                ICollection<UserOperationClaim> uocs = await _userOperationClaimService.GetUserOperationClaimsByUserIdAsync(user!.Id);

                await _userOperationClaimService.DeleteRangeAsync(uocs, true);

                UserStateOperationClaimDto userStateOperationClaimDto = await _userOperationClaimService.SetUserOperationClaimsAsync(user!, Domain.Enums.UserState.Admin);

                response.Success = true;
            }
            else
            {
                response.Success = false;
            }
            return response;
        }
    }
}
