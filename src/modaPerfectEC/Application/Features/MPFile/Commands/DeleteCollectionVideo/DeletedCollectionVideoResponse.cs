using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.MPFile.Commands.DeleteCollectionVideo;
public class DeletedCollectionVideoResponse
{
    public Guid Id { get; set; }
    public string CollectionName { get; set; }
}
