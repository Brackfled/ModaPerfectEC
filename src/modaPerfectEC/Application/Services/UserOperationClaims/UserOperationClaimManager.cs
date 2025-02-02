using System.Linq.Expressions;
using Application.Features.OperationClaims.Rules;
using Application.Features.UserOperationClaims.Rules;
using Application.Services.Repositories;
using Domain.Dtos;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore.Query;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Services.UserOperationClaims;

public class UserUserOperationClaimManager : IUserOperationClaimService
{
    private readonly IUserOperationClaimRepository _userUserOperationClaimRepository;
    private readonly UserOperationClaimBusinessRules _userUserOperationClaimBusinessRules;
    private readonly OperationClaimBusinessRules _operationClaimBusinessRules;

    public UserUserOperationClaimManager(IUserOperationClaimRepository userUserOperationClaimRepository, UserOperationClaimBusinessRules userUserOperationClaimBusinessRules, OperationClaimBusinessRules operationClaimBusinessRules)
    {
        _userUserOperationClaimRepository = userUserOperationClaimRepository;
        _userUserOperationClaimBusinessRules = userUserOperationClaimBusinessRules;
        _operationClaimBusinessRules = operationClaimBusinessRules;
    }

    public async Task<UserOperationClaim?> GetAsync(
        Expression<Func<UserOperationClaim, bool>> predicate,
        Func<IQueryable<UserOperationClaim>, IIncludableQueryable<UserOperationClaim, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        UserOperationClaim? userUserOperationClaim = await _userUserOperationClaimRepository.GetAsync(
            predicate,
            include,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return userUserOperationClaim;
    }

    public async Task<IPaginate<UserOperationClaim>?> GetListAsync(
        Expression<Func<UserOperationClaim, bool>>? predicate = null,
        Func<IQueryable<UserOperationClaim>, IOrderedQueryable<UserOperationClaim>>? orderBy = null,
        Func<IQueryable<UserOperationClaim>, IIncludableQueryable<UserOperationClaim, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<UserOperationClaim> userUserOperationClaimList = await _userUserOperationClaimRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return userUserOperationClaimList;
    }

    public async Task<UserOperationClaim> AddAsync(UserOperationClaim userUserOperationClaim)
    {
        await _userUserOperationClaimBusinessRules.UserShouldNotHasOperationClaimAlreadyWhenInsert(
            userUserOperationClaim.UserId,
            userUserOperationClaim.OperationClaimId
        );

        UserOperationClaim addedUserOperationClaim = await _userUserOperationClaimRepository.AddAsync(userUserOperationClaim);

        return addedUserOperationClaim;
    }

    public async Task<UserOperationClaim> UpdateAsync(UserOperationClaim userUserOperationClaim)
    {
        await _userUserOperationClaimBusinessRules.UserShouldNotHasOperationClaimAlreadyWhenUpdated(
            userUserOperationClaim.Id,
            userUserOperationClaim.UserId,
            userUserOperationClaim.OperationClaimId
        );

        UserOperationClaim updatedUserOperationClaim = await _userUserOperationClaimRepository.UpdateAsync(
            userUserOperationClaim
        );

        return updatedUserOperationClaim;
    }

    public async Task<UserOperationClaim> DeleteAsync(UserOperationClaim userUserOperationClaim, bool permanent = false)
    {
        UserOperationClaim deletedUserOperationClaim = await _userUserOperationClaimRepository.DeleteAsync(
            userUserOperationClaim
        );

        return deletedUserOperationClaim;
    }

    public async Task<UserStateOperationClaimDto> SetUserOperationClaimsAsync(User user, UserState userState)
    {
        int[] approvedUser = [516, 518, 517, 514, 508, 493, 499];
        int[] adminUser = [516, 518, 517, 514, 508, 513, 507, 480, 483, 485, 484, 486, 489, 490, 491, 504, 505, 506, 492, 495, 496, 497, 493, 498, 501, 502, 503, 499, 479];

        UserStateOperationClaimDto userStateOperationClaimDto = new UserStateOperationClaimDto();

        if (userState == UserState.Confirmed)
        {
            foreach (int oc in approvedUser)
            {
                await _operationClaimBusinessRules.OperationClaimIdShouldExistWhenSelected(oc);

                UserOperationClaim uoc = new()
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    OperationClaimId = oc,
                };

                await _userUserOperationClaimRepository.AddAsync(uoc);

                userStateOperationClaimDto.UserState = UserState.Confirmed;
                userStateOperationClaimDto.Success = true;
            }
        }

        if (userState == UserState.Admin)
        {
            foreach (int oc in adminUser)
            {
                await _operationClaimBusinessRules.OperationClaimIdShouldExistWhenSelected(oc);

                UserOperationClaim uoc = new()
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    OperationClaimId = oc,
                };

                await _userUserOperationClaimRepository.AddAsync(uoc);
                user.UserState = UserState.Admin;
                userStateOperationClaimDto.Success = true;
            }

            
        }

        return userStateOperationClaimDto;
    }

    public async Task<IList<UserOperationClaim>> GetUserOperationClaimsByUserIdAsync(Guid userId)
    {
        IList<UserOperationClaim> userOperationClaims = await _userUserOperationClaimRepository.GetUserOperationClaimsByUserIdAsync(userId);
        return userOperationClaims;
    }

    public async Task<ICollection<UserOperationClaim>> DeleteRangeAsync(ICollection<UserOperationClaim> userOperationClaims, bool permantly)
    {
        ICollection<UserOperationClaim> uocs = await _userUserOperationClaimRepository.DeleteRangeAsync(userOperationClaims, permantly);
        return uocs;

    }
}
