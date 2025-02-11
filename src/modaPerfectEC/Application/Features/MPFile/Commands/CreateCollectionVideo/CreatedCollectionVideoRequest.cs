using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.MPFile.Commands.CreateCollectionVideo;
public class CreatedCollectionVideoRequest
{
    public string CollectionName { get; set; }
    public IList<IFormFile> FormFiles { get; set; }
}
