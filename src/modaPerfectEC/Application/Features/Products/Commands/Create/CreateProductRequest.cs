using Domain.Dtos;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Commands.Create;
public class CreateProductRequest
{
    public  int CategoryId { get; set; }
    public  int SubCategoryId { get; set; }
    public  string Name { get; set; }
    public  double Price { get; set; }
    public  string Description { get; set; }
    public  ProductState ProductState { get; set; }
    public ColorDto[] ColorDtos { get; set; }
    public int[] Sizes { get; set; }

}
