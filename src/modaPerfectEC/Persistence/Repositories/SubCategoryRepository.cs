using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class SubCategoryRepository : EfRepositoryBase<SubCategory, int, BaseDbContext>, ISubCategoryRepository
{
    public SubCategoryRepository(BaseDbContext context) : base(context)
    {
    }
}