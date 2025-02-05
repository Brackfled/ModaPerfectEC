using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using NArchitecture.Core.Persistence.Repositories;
using System.Linq.Expressions;

namespace Application.Services.Repositories;

public interface IOrderRepository : IAsyncRepository<Order, Guid>, IRepository<Order, Guid>
{
    Task<ICollection<Order>> GetAllAsync(Expression<Func<Order, bool>>? predicate = null, Func<IQueryable<Order>, IIncludableQueryable<Order, object>>? include = null);
}