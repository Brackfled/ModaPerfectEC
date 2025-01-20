using NArchitecture.Core.Application.Dtos;

namespace Application.Features.ProductVariants.Queries.GetList;

public class GetListProductVariantListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string Color { get; set; }
    public int StockAmount { get; set; }
}