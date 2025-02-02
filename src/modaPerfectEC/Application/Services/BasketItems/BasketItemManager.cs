using Application.Features.BasketItems.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using Application.Services.Baskets;
using Application.Features.Baskets.Rules;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.BasketItems;

public class BasketItemManager : IBasketItemService
{
    private readonly IBasketItemRepository _basketItemRepository;
    private readonly IBasketService _basketService;
    private readonly BasketItemBusinessRules _basketItemBusinessRules;
    private readonly BasketBusinessRules _basketBusinessRules;

    public BasketItemManager(IBasketItemRepository basketItemRepository, IBasketService basketService, BasketItemBusinessRules basketItemBusinessRules, BasketBusinessRules basketBusinessRules)
    {
        _basketItemRepository = basketItemRepository;
        _basketService = basketService;
        _basketItemBusinessRules = basketItemBusinessRules;
        _basketBusinessRules = basketBusinessRules;
    }

    public async Task<BasketItem?> GetAsync(
        Expression<Func<BasketItem, bool>> predicate,
        Func<IQueryable<BasketItem>, IIncludableQueryable<BasketItem, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        BasketItem? basketItem = await _basketItemRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return basketItem;
    }

    public async Task<IPaginate<BasketItem>?> GetListAsync(
        Expression<Func<BasketItem, bool>>? predicate = null,
        Func<IQueryable<BasketItem>, IOrderedQueryable<BasketItem>>? orderBy = null,
        Func<IQueryable<BasketItem>, IIncludableQueryable<BasketItem, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<BasketItem> basketItemList = await _basketItemRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return basketItemList;
    }

    public async Task<BasketItem> AddAsync(BasketItem basketItem)
    {
        BasketItem addedBasketItem = await _basketItemRepository.AddAsync(basketItem);

        return addedBasketItem;
    }

    public async Task<BasketItem> UpdateAsync(BasketItem basketItem)
    {
        BasketItem updatedBasketItem = await _basketItemRepository.UpdateAsync(basketItem);

        return updatedBasketItem;
    }

    public async Task<BasketItem> DeleteAsync(BasketItem basketItem, bool permanent = false)
    {
        BasketItem deletedBasketItem = await _basketItemRepository.DeleteAsync(basketItem);

        return deletedBasketItem;
    }

    public async Task<ICollection<BasketItem>> DeleteAllByProductIdAsync(Guid productId)
    {
        ICollection<BasketItem> basketItems = await _basketItemRepository.GetAllAsync(
            predicate: bi => bi.ProductId == productId,
            include: opt => opt.Include(bi => bi.Product)!.Include(bi => bi.ProductVariant)!
            );

        foreach (BasketItem basketItem in basketItems)
        {
            Basket? basket = await _basketService.GetAsync(b => b.Id == basketItem.BasketId);
            await _basketBusinessRules.BasketShouldExistWhenSelected(basket);

            basket!.TotalPrice = Math.Round(basket.TotalPrice - ((basketItem!.ProductAmount * basketItem!.Product!.Price) * basketItem.ProductVariant!.Sizes.Length), 2, MidpointRounding.AwayFromZero);
            Math.Round(basket.TotalPriceUSD - ((basketItem!.ProductAmount * basketItem!.Product!.PriceUSD) * basketItem.ProductVariant.Sizes.Length), 2, MidpointRounding.AwayFromZero);

            await _basketService.UpdateAsync(basket);
        }

        ICollection<BasketItem> deletedBasketItems = await _basketItemRepository.DeleteRangeAsync(basketItems, true);
        return deletedBasketItems;
    }
}
