using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Category: Entity<int>
{
    public string Name { get; set; }

    public virtual ICollection<Product>? Products { get; set; }
    public virtual ICollection<SubCategory>? SubCategories { get; set; }

    public Category()
    {
        Name = string.Empty;
    }

    public Category(string name, ICollection<Product> products, ICollection<SubCategory> subCategories)
    {
        Name = name;
        Products = products;
        SubCategories = subCategories;
    }
}
