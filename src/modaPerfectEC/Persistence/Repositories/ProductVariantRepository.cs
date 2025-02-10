using Application.Services.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;
using System.Linq.Expressions;

namespace Persistence.Repositories;

public class ProductVariantRepository : EfRepositoryBase<ProductVariant, Guid, BaseDbContext>, IProductVariantRepository
{
    public ProductVariantRepository(BaseDbContext context) : base(context)
    {
    }

    public async Task<ICollection<ProductVariant>> GetAllAsync(Expression<Func<ProductVariant, bool>>? predicate = null, Func<IQueryable<ProductVariant>, IIncludableQueryable<ProductVariant, object>>? include = null)
    {
        IQueryable<ProductVariant> query = Query();

        if (predicate != null)
            query = query.Where(predicate);
        if (include != null)
            query = include(query);

        query = query.AsNoTracking();
        return await query.ToListAsync();
    }

    public async Task<ProductVariant> UpdateStockAmount(ProductVariant productVariant, bool increase, int processAmount)
    {
        if (increase)
            productVariant.StockAmount = productVariant.StockAmount + processAmount;
        if(!increase)
            productVariant.StockAmount = productVariant.StockAmount - processAmount;

        ProductVariant updatedProductVariant = await UpdateAsync(productVariant);
        return updatedProductVariant;
    }
}