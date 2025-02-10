using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using NArchitecture.Core.Persistence.Repositories;
using System.Linq.Expressions;

namespace Application.Services.Repositories;

public interface IProductVariantRepository : IAsyncRepository<ProductVariant, Guid>, IRepository<ProductVariant, Guid>
{
    Task<ProductVariant> UpdateStockAmount(ProductVariant productVariant,bool increase, int processAmount);
    Task<ICollection<ProductVariant>> GetAllAsync(Expression<Func<ProductVariant, bool>>? predicate = null, Func<IQueryable<ProductVariant>, IIncludableQueryable<ProductVariant, object>>? include = null);
}