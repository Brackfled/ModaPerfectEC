using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.MPFile.Commands.CreateProductImage;
public class CreateProductImageRequest
{
    public Guid ProductId { get; set; }
    public IList<IFormFile> FormFiles { get; set; }
}
