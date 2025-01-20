using Application.Services.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using NArchitecture.Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ProductImages;
public class ProductImageManager: IProductImageService
{
    private readonly IProductImageRepository _productImageRepository;

    public ProductImageManager(IProductImageRepository productImageRepository)
    {
        _productImageRepository = productImageRepository;
    }

    public async Task<ProductImage> AddAsync(ProductImage productImage)
    {
        ProductImage addedProductImage = await _productImageRepository.AddAsync(productImage);
        return addedProductImage;
    }

    public async Task<ProductImage> DeleteAsync(ProductImage productImage, bool permanent = false)
    {
        ProductImage deletedProductImage = await _productImageRepository.DeleteAsync(productImage);
        return deletedProductImage;
    }

    public async Task<ProductImage?> GetAsync(Expression<Func<ProductImage, bool>> predicate, Func<IQueryable<ProductImage>, IIncludableQueryable<ProductImage, object>>? include = null, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        ProductImage? productImage = await _productImageRepository.GetAsync(
            predicate, include, withDeleted, enableTracking, cancellationToken
            );

        return productImage;
    }

    public async Task<IPaginate<ProductImage>?> GetListAsync(Expression<Func<ProductImage, bool>>? predicate = null, Func<IQueryable<ProductImage>, IOrderedQueryable<ProductImage>>? orderBy = null, Func<IQueryable<ProductImage>, IIncludableQueryable<ProductImage, object>>? include = null, int index = 0, int size = 10, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        IPaginate<ProductImage>? productImage = await _productImageRepository.GetListAsync(
                predicate, orderBy, include, size, index, withDeleted, enableTracking, cancellationToken
            );

        return productImage;
    }

    public async Task<ProductImage> UpdateAsync(ProductImage courtImage)
    {
        ProductImage updatedProductImage = await _productImageRepository.UpdateAsync(courtImage);
        return updatedProductImage;
    }
}
