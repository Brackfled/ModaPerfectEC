using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Repositories;
public interface IProductImageRepository : IAsyncRepository<ProductImage, Guid>, IRepository<ProductImage, Guid>
{
    Task<ICollection<ProductImage>> GetAllAsync(Expression<Func<ProductImage, bool>>? predicate = null);
}
