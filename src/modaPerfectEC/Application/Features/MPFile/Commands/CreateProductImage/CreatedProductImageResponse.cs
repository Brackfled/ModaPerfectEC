using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.MPFile.Commands.CreateProductImage;
public class CreatedProductImageResponse
{
    public Guid ProductId { get; set; }
    public IList<ProductImage> ProductImages { get; set; }
}
