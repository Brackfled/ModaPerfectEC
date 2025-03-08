using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ProductReturns;

public interface IProductReturnService
{
    Task<ProductReturn?> GetAsync(
        Expression<Func<ProductReturn, bool>> predicate,
        Func<IQueryable<ProductReturn>, IIncludableQueryable<ProductReturn, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<ProductReturn>?> GetListAsync(
        Expression<Func<ProductReturn, bool>>? predicate = null,
        Func<IQueryable<ProductReturn>, IOrderedQueryable<ProductReturn>>? orderBy = null,
        Func<IQueryable<ProductReturn>, IIncludableQueryable<ProductReturn, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<ProductReturn> AddAsync(ProductReturn productReturn);
    Task<ProductReturn> UpdateAsync(ProductReturn productReturn);
    Task<ProductReturn> DeleteAsync(ProductReturn productReturn, bool permanent = false);
}
