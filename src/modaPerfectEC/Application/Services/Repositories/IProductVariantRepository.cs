using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IProductVariantRepository : IAsyncRepository<ProductVariant, Guid>, IRepository<ProductVariant, Guid>
{
}