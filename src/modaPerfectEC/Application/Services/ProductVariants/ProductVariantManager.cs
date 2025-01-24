using Application.Features.ProductVariants.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ProductVariants;

public class ProductVariantManager : IProductVariantService
{
    private readonly IProductVariantRepository _productVariantRepository;
    private readonly ProductVariantBusinessRules _productVariantBusinessRules;

    public ProductVariantManager(IProductVariantRepository productVariantRepository, ProductVariantBusinessRules productVariantBusinessRules)
    {
        _productVariantRepository = productVariantRepository;
        _productVariantBusinessRules = productVariantBusinessRules;
    }

    public async Task<ProductVariant?> GetAsync(
        Expression<Func<ProductVariant, bool>> predicate,
        Func<IQueryable<ProductVariant>, IIncludableQueryable<ProductVariant, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        ProductVariant? productVariant = await _productVariantRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return productVariant;
    }

    public async Task<IPaginate<ProductVariant>?> GetListAsync(
        Expression<Func<ProductVariant, bool>>? predicate = null,
        Func<IQueryable<ProductVariant>, IOrderedQueryable<ProductVariant>>? orderBy = null,
        Func<IQueryable<ProductVariant>, IIncludableQueryable<ProductVariant, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<ProductVariant> productVariantList = await _productVariantRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return productVariantList;
    }

    public async Task<ProductVariant> AddAsync(ProductVariant productVariant)
    {
        ProductVariant addedProductVariant = await _productVariantRepository.AddAsync(productVariant);

        return addedProductVariant;
    }

    public async Task<ProductVariant> UpdateAsync(ProductVariant productVariant)
    {
        ProductVariant updatedProductVariant = await _productVariantRepository.UpdateAsync(productVariant);

        return updatedProductVariant;
    }

    public async Task<ProductVariant> DeleteAsync(ProductVariant productVariant, bool permanent = false)
    {
        ProductVariant deletedProductVariant = await _productVariantRepository.DeleteAsync(productVariant);

        return deletedProductVariant;
    }

    public async Task<ProductVariant> UpdateStockAmount(ProductVariant productVariant, bool increase, int processAmount)
    {
        ProductVariant updatedProductVariant = await _productVariantRepository.UpdateStockAmount(productVariant, increase, processAmount);
        return updatedProductVariant;
    }
}
