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
public interface IProductImageService
{
    Task<ProductImage?> GetAsync(
        Expression<Func<ProductImage, bool>> predicate,
        Func<IQueryable<ProductImage>, IIncludableQueryable<ProductImage, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );

    Task<IPaginate<ProductImage>?> GetListAsync(
        Expression<Func<ProductImage, bool>>? predicate = null,
        Func<IQueryable<ProductImage>, IOrderedQueryable<ProductImage>>? orderBy = null,
        Func<IQueryable<ProductImage>, IIncludableQueryable<ProductImage, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );

    Task<ProductImage> AddAsync(ProductImage productImage);
    Task<ProductImage> UpdateAsync(ProductImage productImage);
    Task<ProductImage> DeleteAsync(ProductImage productImage, bool permanent = false);
    Task<ICollection<ProductImage>> GetAllAsync(Expression<Func<ProductImage, bool>>? predicate = null);
}
