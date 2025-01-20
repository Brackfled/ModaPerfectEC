using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class ProductImage: MPFile
{
    public Guid ProductId { get; set; }

    public virtual Product? Product { get; set; }
}
