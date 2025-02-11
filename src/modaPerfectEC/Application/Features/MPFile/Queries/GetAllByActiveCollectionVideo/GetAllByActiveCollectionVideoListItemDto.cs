﻿using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.MPFile.Queries.GetAllByActiveCollectionVideo;
public class GetAllByActiveCollectionVideoListItemDto
{
    public Guid Id { get; set; }
    public string CollectionName { get; set; }
    public CollectionState CollectionState { get; set; }
    public string FileUrl { get; set; }
}
