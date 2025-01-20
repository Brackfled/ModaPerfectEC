using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class MPFile : Entity<Guid>
{
    public string FileName { get; set; }
    public string FilePath { get; set; }
    public string FileUrl { get; set; }
}
