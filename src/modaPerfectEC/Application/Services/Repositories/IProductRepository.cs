using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using NArchitecture.Core.Persistence.Repositories;
using System.Linq.Expressions;
using static Amazon.S3.Util.S3EventNotification;

namespace Application.Services.Repositories;

public interface IProductRepository : IAsyncRepository<Product, Guid>, IRepository<Product, Guid>
{
    public Task<ICollection<Product>> GetAllAsync(Expression<Func<Product, bool>>? predicate = null, Func<IQueryable<Product>, IIncludableQueryable<Product, object>>? include = null);
}