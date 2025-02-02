using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using NArchitecture.Core.Persistence.Repositories;
using System.Linq.Expressions;

namespace Application.Services.Repositories;

public interface IBasketItemRepository : IAsyncRepository<BasketItem, Guid>, IRepository<BasketItem, Guid>
{
    public Task<ICollection<BasketItem>> GetAllAsync(Expression<Func<BasketItem, bool>>? predicate = null, Func<IQueryable<BasketItem>, IIncludableQueryable<BasketItem, object>>? include = null);

    Task<ICollection<BasketItem>> DeleteAllByProductIdAsync(Guid productId);
}