using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class CollectionVideo: MPFile
{
    public string CollectionName { get; set; }
    public CollectionState CollectionState { get; set; }

    public CollectionVideo()
    {
        CollectionName = string.Empty;
    }

    public CollectionVideo(string collectionName)
    {
        CollectionName = collectionName;
    }
}
