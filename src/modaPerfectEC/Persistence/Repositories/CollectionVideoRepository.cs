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
public class CollectionVideoRepository: EfRepositoryBase<CollectionVideo, Guid, BaseDbContext>, ICollectionVideoRepository
{
    public CollectionVideoRepository(BaseDbContext context): base(context)
    {
        
    }

    public async Task<ICollection<CollectionVideo>> GetAllAsync(Expression<Func<CollectionVideo, bool>>? predicate = null)
    {
        IQueryable<CollectionVideo> query = Query();

        if (predicate != null)
            query = query.Where(predicate);

        query = query.AsNoTracking();
        return await query.ToListAsync();
    }
}
