using NArchitecture.Core.Application.Dtos;
using Domain.Enums;
using Domain.Entities;

namespace Application.Features.Products.Queries.GetList;

public class GetListProductListItemDto : IDto
{
    public Guid Id { get; set; }
    public int CategoryId { get; set; }
    public int SubCategoryId { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public string Description { get; set; }
    public ProductState ProductState { get; set; }
    public ICollection<ProductVariant>? ProductVariants { get; set; }
    public ICollection<ProductImage>? ProductImages { get; set; }
}