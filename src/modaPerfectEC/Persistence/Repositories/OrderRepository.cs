using Application.Services.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;
using System.Linq.Expressions;

namespace Persistence.Repositories;

public class OrderRepository : EfRepositoryBase<Order, Guid, BaseDbContext>, IOrderRepository
{
    public OrderRepository(BaseDbContext context) : base(context)
    {
    }

    public async Task<ICollection<Order>> GetAllAsync(Expression<Func<Order, bool>>? predicate = null, Func<IQueryable<Order>, IIncludableQueryable<Order, object>>? include = null)
    {
        IQueryable<Order> query = Query();

        if (predicate != null)
            query = query.Where(predicate);
        if (include != null)
            query = include(query);

        query = query.AsNoTracking();
        return await query.ToListAsync();
    }
}