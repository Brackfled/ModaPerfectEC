using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ProductReturnRepository : EfRepositoryBase<ProductReturn, Guid, BaseDbContext>, IProductReturnRepository
{
    public ProductReturnRepository(BaseDbContext context) : base(context)
    {
    }
}