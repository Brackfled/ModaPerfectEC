using Amazon.Runtime.Internal;
using Application.Services.Repositories;
using Application.Services.UserOperationClaims;
using Application.Services.UsersService;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Transaction;
using NArchitecture.Core.Persistence.Paging;
using NArchitecture.Core.Security.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Commands.Refresh;
public class RefreshUserOperationClaimCommand : IRequest<RefreshedUserOperationClaimResponse>, ITransactionalRequest, ISecuredRequest
{
    public string[] Roles => [GeneralOperationClaims.Admin];

    public class RefreshUserOperationClaimCommandHandler : IRequestHandler<RefreshUserOperationClaimCommand, RefreshedUserOperationClaimResponse>
    {
        private readonly IUserService _userService;
        private readonly IUserOperationClaimService _userOperationClaimService;
        private IMapper _mapper;

        public RefreshUserOperationClaimCommandHandler(IUserService userService, IUserOperationClaimService userOperationClaimService, IMapper mapper)
        {
            _userService = userService;
            _userOperationClaimService = userOperationClaimService;
            _mapper = mapper;
        }

        public async Task<RefreshedUserOperationClaimResponse> Handle(RefreshUserOperationClaimCommand request, CancellationToken cancellationToken)
        {
            bool succes = true;

            IPaginate<User>? users = await _userService.GetListAsync(
                    index: 0,
                    size: 1000,
                    cancellationToken: cancellationToken
                );

            foreach (User user in users!.Items)
            {
                IList<UserOperationClaim> userOperationClaims = await _userOperationClaimService.GetUserOperationClaimsByUserIdAsync( user.Id );

                await _userOperationClaimService.DeleteRangeAsync(userOperationClaims, true);

               UserStateOperationClaimDto dto = await _userOperationClaimService.SetUserOperationClaimsAsync(user, user.UserState);

                if(!dto.Success)
                    succes = false;
            }

            RefreshedUserOperationClaimResponse response = new() { Success = succes };
            return response;
        }
    }
}
