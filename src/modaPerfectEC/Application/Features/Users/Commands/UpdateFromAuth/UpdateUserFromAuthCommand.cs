using Application.Features.Baskets.Rules;
using Application.Features.Users.Rules;
using Application.Services.AuthService;
using Application.Services.Baskets;
using Application.Services.Repositories;
using Application.Services.UserOperationClaims;
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
        private readonly IUserOperationClaimService _userOperationClaimService;
        private readonly IBasketService _basketService;
        private readonly BasketBusinessRules _basketBusinessRules;

        public UpdateUserFromAuthCommandHandler(IUserRepository userRepository, IMapper mapper, UserBusinessRules userBusinessRules, IAuthService authService, IUserOperationClaimService userOperationClaimService, IBasketService basketService, BasketBusinessRules basketBusinessRules)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userBusinessRules = userBusinessRules;
            _authService = authService;
            _userOperationClaimService = userOperationClaimService;
            _basketService = basketService;
            _basketBusinessRules = basketBusinessRules;
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

            IList<UserOperationClaim> uocs = await _userOperationClaimService.GetUserOperationClaimsByUserIdAsync(user.Id);
            await _userOperationClaimService.DeleteRangeAsync(uocs, true);

            Basket? basket = await _basketService.GetAsync(b => b.UserId == user.Id && b.IsOrderBasket == false);
            await _basketBusinessRules.BasketShouldExistWhenSelected(basket);

            await _basketService.DeleteAsync(basket!, true);

            UpdatedUserFromAuthResponse response = _mapper.Map<UpdatedUserFromAuthResponse>(updatedUser);
            return response;
        }
    }
}
