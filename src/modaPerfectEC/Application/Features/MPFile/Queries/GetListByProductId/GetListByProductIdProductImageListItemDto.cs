﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.MPFile.Queries.GetListByProductId;
public class GetListByProductIdProductImageListItemDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string FileName { get; set; }
    public string FilePath { get; set; }
    public string FileUrl { get; set; }
    public DateTime CreatedDate { get; set; }
}
