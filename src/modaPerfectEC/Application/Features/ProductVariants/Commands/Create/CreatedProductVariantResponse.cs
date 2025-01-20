using NArchitecture.Core.Application.Responses;

namespace Application.Features.ProductVariants.Commands.Create;

public class CreatedProductVariantResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string Color { get; set; }
    public int StockAmount { get; set; }
}