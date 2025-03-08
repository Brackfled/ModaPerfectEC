using Application.Features.ProductReturns.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ProductReturns;

public class ProductReturnManager : IProductReturnService
{
    private readonly IProductReturnRepository _productReturnRepository;
    private readonly ProductReturnBusinessRules _productReturnBusinessRules;

    public ProductReturnManager(IProductReturnRepository productReturnRepository, ProductReturnBusinessRules productReturnBusinessRules)
    {
        _productReturnRepository = productReturnRepository;
        _productReturnBusinessRules = productReturnBusinessRules;
    }

    public async Task<ProductReturn?> GetAsync(
        Expression<Func<ProductReturn, bool>> predicate,
        Func<IQueryable<ProductReturn>, IIncludableQueryable<ProductReturn, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        ProductReturn? productReturn = await _productReturnRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return productReturn;
    }

    public async Task<IPaginate<ProductReturn>?> GetListAsync(
        Expression<Func<ProductReturn, bool>>? predicate = null,
        Func<IQueryable<ProductReturn>, IOrderedQueryable<ProductReturn>>? orderBy = null,
        Func<IQueryable<ProductReturn>, IIncludableQueryable<ProductReturn, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<ProductReturn> productReturnList = await _productReturnRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return productReturnList;
    }

    public async Task<ProductReturn> AddAsync(ProductReturn productReturn)
    {
        ProductReturn addedProductReturn = await _productReturnRepository.AddAsync(productReturn);

        return addedProductReturn;
    }

    public async Task<ProductReturn> UpdateAsync(ProductReturn productReturn)
    {
        ProductReturn updatedProductReturn = await _productReturnRepository.UpdateAsync(productReturn);

        return updatedProductReturn;
    }

    public async Task<ProductReturn> DeleteAsync(ProductReturn productReturn, bool permanent = false)
    {
        ProductReturn deletedProductReturn = await _productReturnRepository.DeleteAsync(productReturn);

        return deletedProductReturn;
    }
}
