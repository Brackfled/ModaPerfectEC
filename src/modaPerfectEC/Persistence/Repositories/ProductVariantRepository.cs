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

    public async Task<ProductVariant> UpdateStockAmount(ProductVariant productVariant, bool increase, int processAmount)
    {
        if (increase)
            productVariant.StockAmount = productVariant.StockAmount + processAmount;
        if(!increase)
            productVariant.StockAmount = productVariant.StockAmount - processAmount;

        ProductVariant updatedProductVariant = await UpdateAsync(productVariant);
        return updatedProductVariant;
    }
}