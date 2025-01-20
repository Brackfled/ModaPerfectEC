using NArchitecture.Core.Application.Responses;
using Domain.Enums;

namespace Application.Features.Products.Commands.Update;

public class UpdatedProductResponse : IResponse
{
    public Guid Id { get; set; }
    public int CategoryId { get; set; }
    public int SubCategoryId { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public string Description { get; set; }
    public ProductState ProductState { get; set; }
}