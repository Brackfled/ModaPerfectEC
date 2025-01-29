using Application.Features.Baskets.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Baskets.Rules;

public class BasketBusinessRules : BaseBusinessRules
{
    private readonly IBasketRepository _basketRepository;
    private readonly ILocalizationService _localizationService;

    public BasketBusinessRules(IBasketRepository basketRepository, ILocalizationService localizationService)
    {
        _basketRepository = basketRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, BasketsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task BasketShouldExistWhenSelected(Basket? basket)
    {
        if (basket == null)
            await throwBusinessException(BasketsBusinessMessages.BasketNotExists);
    }

    public async Task BasketIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Basket? basket = await _basketRepository.GetAsync(
            predicate: b => b.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await BasketShouldExistWhenSelected(basket);
    }

    public async Task UserShouldNotHasActiveBasket(Guid userId)
    {
        bool doesExists = await _basketRepository.AnyAsync(b => b.UserId == userId && b.IsOrderBasket == false);

        if (doesExists)
            await throwBusinessException(BasketsBusinessMessages.UserHasABasket);
    }

    public async Task UserShouldHasOneActiveBasket(Guid userId )
    {
        IPaginate<Basket> baskets = await _basketRepository.GetListAsync(
                predicate: b => b.UserId == userId && b.IsOrderBasket == false,
                size:1000,
                index:0
            );

        if (baskets.Count > 1)
            await throwBusinessException(BasketsBusinessMessages.UserHasABasket);
    }
}