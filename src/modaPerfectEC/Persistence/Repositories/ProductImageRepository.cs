using Application.Services.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories;
public class ProductImageRepository: EfRepositoryBase<ProductImage, Guid, BaseDbContext>, IProductImageRepository
{
    public ProductImageRepository(BaseDbContext context): base(context)
    {
        
    }

    public async Task<ICollection<ProductImage>> GetAllAsync(Expression<Func<ProductImage, bool>>? predicate = null)
    {
        IQueryable<ProductImage> query = Query();

        if (predicate != null)
            query = query.Where(predicate);

        query = query.AsNoTracking();
        return await query.ToListAsync();
    }
}
