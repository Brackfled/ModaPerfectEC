using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.MPFile.Commands.UpdateCollectionStateCollectionVideo;
public class UpdatedCollectionStateCollectionVideoResponse
{
    public Guid Id { get; set; }
    public string CollectionName { get; set; }
    public CollectionState CollectionState { get; set; }
}
