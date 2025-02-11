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

namespace Application.Services.CollectionVideos;
public class CollectionVideoManager : ICollectionVideoService
{
    private readonly ICollectionVideoRepository _collectionVideoRepository;

    public CollectionVideoManager(ICollectionVideoRepository collectionVideoRepository)
    {
        _collectionVideoRepository = collectionVideoRepository;
    }

    public async Task<CollectionVideo> AddAsync(CollectionVideo collectionVideo)
    {
        CollectionVideo addedCollectionVideo = await _collectionVideoRepository.AddAsync(collectionVideo);
        return addedCollectionVideo;
    }

    public async Task<CollectionVideo> DeleteAsync(CollectionVideo collectionVideo, bool permanent = false)
    {
        CollectionVideo deletedCollectionVideo = await _collectionVideoRepository.DeleteAsync(collectionVideo, permanent);
        return deletedCollectionVideo;
    }

    public async Task<ICollection<CollectionVideo>> GetAllAsync(Expression<Func<CollectionVideo, bool>>? predicate = null)
    {
        ICollection<CollectionVideo> collectionVideos = await _collectionVideoRepository.GetAllAsync(predicate);
        return collectionVideos;
    }

    public async Task<CollectionVideo?> GetAsync(Expression<Func<CollectionVideo, bool>> predicate, Func<IQueryable<CollectionVideo>, IIncludableQueryable<CollectionVideo, object>>? include = null, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        CollectionVideo? collectionVideo = await _collectionVideoRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return collectionVideo;
    }

    public async Task<IPaginate<CollectionVideo>?> GetListAsync(Expression<Func<CollectionVideo, bool>>? predicate = null, Func<IQueryable<CollectionVideo>, IOrderedQueryable<CollectionVideo>>? orderBy = null, Func<IQueryable<CollectionVideo>, IIncludableQueryable<CollectionVideo, object>>? include = null, int index = 0, int size = 10, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        IPaginate<CollectionVideo> collectionVideos = await _collectionVideoRepository.GetListAsync(
                predicate, orderBy, include, index, size, withDeleted, enableTracking, cancellationToken
            );
        return collectionVideos;
    }

    public async Task<CollectionVideo> UpdateAsync(CollectionVideo collectionVideo)
    {
        CollectionVideo updatedCollectionVideo = await _collectionVideoRepository.UpdateAsync(collectionVideo);
        return updatedCollectionVideo;
    }
}
