using Domain.Entities;
using NArchitecture.Core.Application.Responses;

namespace Application.Features.ProductVariants.Commands.Update;

public class UpdatedProductVariantResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string Color { get; set; }
    public string Hex { get; set; }
    public int StockAmount { get; set; }
    public int[] Sizes { get; set; }
    public virtual Product? Product { get; set; }
}