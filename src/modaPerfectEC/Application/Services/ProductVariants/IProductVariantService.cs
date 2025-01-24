using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ProductVariants;

public interface IProductVariantService
{
    Task<ProductVariant?> GetAsync(
        Expression<Func<ProductVariant, bool>> predicate,
        Func<IQueryable<ProductVariant>, IIncludableQueryable<ProductVariant, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<ProductVariant>?> GetListAsync(
        Expression<Func<ProductVariant, bool>>? predicate = null,
        Func<IQueryable<ProductVariant>, IOrderedQueryable<ProductVariant>>? orderBy = null,
        Func<IQueryable<ProductVariant>, IIncludableQueryable<ProductVariant, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<ProductVariant> AddAsync(ProductVariant productVariant);
    Task<ProductVariant> UpdateAsync(ProductVariant productVariant);
    Task<ProductVariant> DeleteAsync(ProductVariant productVariant, bool permanent = false);
    Task<ProductVariant> UpdateStockAmount(ProductVariant productVariant, bool increase, int processAmount);
}
