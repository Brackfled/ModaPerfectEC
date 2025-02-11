using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using NArchitecture.Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.CollectionVideos;
public interface ICollectionVideoService
{
    Task<CollectionVideo?> GetAsync(
        Expression<Func<CollectionVideo, bool>> predicate,
        Func<IQueryable<CollectionVideo>, IIncludableQueryable<CollectionVideo, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );

    Task<IPaginate<CollectionVideo>?> GetListAsync(
        Expression<Func<CollectionVideo, bool>>? predicate = null,
        Func<IQueryable<CollectionVideo>, IOrderedQueryable<CollectionVideo>>? orderBy = null,
        Func<IQueryable<CollectionVideo>, IIncludableQueryable<CollectionVideo, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );

    Task<CollectionVideo> AddAsync(CollectionVideo collectionVideo);
    Task<CollectionVideo> UpdateAsync(CollectionVideo collectionVideo);
    Task<CollectionVideo> DeleteAsync(CollectionVideo collectionVideo, bool permanent = false);
    Task<ICollection<CollectionVideo>> GetAllAsync(Expression<Func<CollectionVideo, bool>>? predicate = null);
}
