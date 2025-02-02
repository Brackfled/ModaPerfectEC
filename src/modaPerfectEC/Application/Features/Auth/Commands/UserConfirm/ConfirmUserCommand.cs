using Application.Features.Auth.Constants;
using Application.Features.Baskets.Rules;
using Application.Features.Users.Rules;
using Application.Services.Baskets;
using Application.Services.Repositories;
using Application.Services.UserOperationClaims;
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
        private readonly IBasketService _basketService;
        private readonly BasketBusinessRules _basketBusinessRules;
        private readonly IUserOperationClaimService _userOperationClaimService;
        private IMapper _mapper;

        public ConfirmUserCommandHandler(IUserService userService, UserBusinessRules userBusinessRules, IBasketService basketService, BasketBusinessRules basketBusinessRules, IUserOperationClaimService userOperationClaimService, IMapper mapper)
        {
            _userService = userService;
            _userBusinessRules = userBusinessRules;
            _basketService = basketService;
            _basketBusinessRules = basketBusinessRules;
            _userOperationClaimService = userOperationClaimService;
            _mapper = mapper;
        }

        public async Task<ConfirmedUserResponse> Handle(ConfirmUserCommand request, CancellationToken cancellationToken)
        {
            await _userBusinessRules.UserIdShouldBeExistsWhenSelected(request.UserId);

            User? user = await _userService.GetAsync(u => u.Id == request.UserId);

            if (user!.UserState == UserState.Pending && request.UserState == UserState.Confirmed)
            {
                await _basketBusinessRules.UserShouldNotHasActiveBasket(request.UserId);

                Basket basket = new()
                {
                    Id = Guid.NewGuid(),
                    IsOrderBasket = false,
                    UserId = request.UserId,
                    TotalPrice = 0.00,
                };

                Basket addedBasket = await _basketService.AddAsync(basket);

                IList<UserOperationClaim> uocs = await _userOperationClaimService.GetUserOperationClaimsByUserIdAsync(user.Id);
                await _userOperationClaimService.DeleteRangeAsync(uocs, true);

                await _userOperationClaimService.SetUserOperationClaimsAsync(user, UserState.Confirmed);

            }

            if(user.UserState == UserState.Confirmed && (request.UserState == UserState.Rejected || request.UserState == UserState.BlackList))
            {
                IList<UserOperationClaim> uocs = await _userOperationClaimService.GetUserOperationClaimsByUserIdAsync(user.Id);

                await _userOperationClaimService.DeleteRangeAsync(uocs, true);
            }

            await _userBusinessRules.UserStateIsAccurate(user, request.UserState);

            user.UserState = request.UserState;

            User updatedUser = await _userService.UpdateAsync(user);          

            ConfirmedUserResponse response = _mapper.Map<ConfirmedUserResponse>(updatedUser);
            return response;
        }
    }
}
