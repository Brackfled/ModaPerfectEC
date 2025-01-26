using NArchitecture.Core.Application.Responses;
using Domain.Enums;
using Domain.Entities;

namespace Application.Features.Products.Commands.Create;

public class CreatedProductResponse : IResponse
{
    public Guid Id { get; set; }
    public int CategoryId { get; set; }
    public int SubCategoryId { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public double PriceUSD { get; set; }
    public string Description { get; set; }
    public ProductState ProductState { get; set; }
    public ICollection<ProductVariant> ProductVariants { get; set; }
}