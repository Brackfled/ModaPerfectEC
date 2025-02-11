using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Repositories;
public interface ICollectionVideoRepository: IAsyncRepository<CollectionVideo, Guid>, IRepository<CollectionVideo, Guid>
{
    Task<ICollection<CollectionVideo>> GetAllAsync(Expression<Func<CollectionVideo, bool>>? predicate = null);
}
