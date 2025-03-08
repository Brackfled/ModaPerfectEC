using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IProductReturnRepository : IAsyncRepository<ProductReturn, Guid>, IRepository<ProductReturn, Guid>
{
}