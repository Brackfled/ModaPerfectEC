using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.MPFile.Commands.CreateCollectionVideo;
public class CreatedCollectionVideoResponse
{
    public string CollectionName { get; set; }
    public ICollection<CollectionVideo> CollectionVideos { get; set; }
}
