using Domain.Entities;
using Domain.Enums;
using NArchitecture.Core.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.GetListByDynamic;
public class GetListByDynamicProductListItemDto: IResponse
{
    public Guid Id { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public int SubCategoryId { get; set; }
    public string SubCategoryName { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public double PriceUSD { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
    public ProductState ProductState { get; set; }
    public ICollection<ProductVariant>? ProductVariants { get; set; }
    public ICollection<ProductImage>? ProductImages { get; set; }
}
