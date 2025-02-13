using Application.Services.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;
using System.Linq.Expressions;

namespace Persistence.Repositories;

public class ProductRepository : EfRepositoryBase<Product, Guid, BaseDbContext>, IProductRepository
{
    public ProductRepository(BaseDbContext context) : base(context)
    {
    }

    public async Task<ICollection<Product>> GetAllAsync(Expression<Func<Product, bool>>? predicate = null, Func<IQueryable<Product>, IIncludableQueryable<Product, object>>? include = null)
    {
        IQueryable<Product> query = Query();

        if (predicate != null)
            query = query.Where(predicate);
        if (include != null)
            query = include(query);

        query = query.AsNoTracking();
        return await query.ToListAsync();
    }

    public async Task<ICollection<Product>> GetMatching(IList<Product> products, string? hex , int? size)
    {
        return products
        .Where(p => p.ProductVariants
            .Any(v =>
                (hex == null || v.Hex == hex) && 
                (size == null || v.Sizes.Contains(size.Value))
            ))
        .ToList();
    }
}