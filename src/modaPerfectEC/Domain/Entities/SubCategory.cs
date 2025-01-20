using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities;
public class SubCategory: Entity<int>
{
    public int CategoryId { get; set; }
    public string Name { get; set; }

    [JsonIgnore]
    public virtual Category? Category { get; set; }
    public virtual ICollection<Product>? Products { get; set; }

    public SubCategory()
    {
        Name = string.Empty;
    }

    public SubCategory(int categoryId, string name, Category? category, ICollection<Product>? products)
    {
        CategoryId = categoryId;
        Name = name;
        Category = category;
        Products = products;
    }
}
