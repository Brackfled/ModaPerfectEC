using Domain.Enums;
using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Product: Entity<Guid>
{
    public int CategoryId { get; set; }
    public int SubCategoryId { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public double PriceUSD { get; set; }
    public string Description { get; set; }
    public ProductState ProductState { get; set; }

    public virtual Category? Category { get; set; }
    public virtual SubCategory? SubCategory { get; set; }
    public virtual ICollection<ProductVariant>? ProductVariants { get; set; }
    public virtual ICollection<ProductImage>? ProductImages { get; set; }

    public Product()
    {
        Name = string.Empty;
        Description = string.Empty;
    }

    public Product(int categoryId, int subCategoryId, string name, double price, string description, ProductState productState, Category? category, SubCategory? subCategory, ICollection<ProductVariant>? productVariants, ICollection<ProductImage>? productImages)
    {
        CategoryId = categoryId;
        SubCategoryId = subCategoryId;
        Name = name;
        Price = price;
        Description = description;
        ProductState = productState;
        Category = category;
        SubCategory = subCategory;
        ProductVariants = productVariants;
        ProductImages = productImages;
    }
}
