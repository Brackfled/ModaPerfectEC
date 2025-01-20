using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ProductVariantRepository : EfRepositoryBase<ProductVariant, Guid, BaseDbContext>, IProductVariantRepository
{
    public ProductVariantRepository(BaseDbContext context) : base(context)
    {
    }
}