using Application.Services.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;
using System.Linq.Expressions;

namespace Persistence.Repositories;

public class BasketItemRepository : EfRepositoryBase<BasketItem, Guid, BaseDbContext>, IBasketItemRepository
{
    public BasketItemRepository(BaseDbContext context) : base(context)
    {
    }

    public async Task<ICollection<BasketItem>> DeleteAllByProductIdAsync(Guid productId)
    {
        IQueryable<BasketItem> query = Query();
        ICollection<BasketItem> findedBasketItems = await query.Where(predicate: bi => bi.ProductId == productId).ToListAsync();

        ICollection<BasketItem> deletedBasketItems = await DeleteRangeAsync(findedBasketItems, true);
        return deletedBasketItems;
    }

    public async Task<ICollection<BasketItem>> GetAllAsync(Expression<Func<BasketItem, bool>>? predicate = null, Func<IQueryable<BasketItem>, IIncludableQueryable<BasketItem, object>>? include = null)
    {
        IQueryable<BasketItem> query = Query();

        if (predicate != null)
            query = query.Where(predicate);
        if (include != null)
            query = include(query);

        query = query.AsNoTracking();
        return await query.ToListAsync();
    }
}