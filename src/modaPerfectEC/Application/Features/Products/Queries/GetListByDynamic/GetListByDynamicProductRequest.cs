using NArchitecture.Core.Application.Dtos;
using NArchitecture.Core.Persistence.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.GetListByDynamic;
public class GetListByDynamicProductRequest: IDto
{
    public DynamicQuery DynamicQuery { get; set; }
    public string? Hex { get; set; }
    public int? Size { get; set; }
}
