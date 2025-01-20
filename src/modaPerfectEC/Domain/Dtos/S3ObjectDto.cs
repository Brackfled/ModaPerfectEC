using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos;
public class S3ObjectDto
{
    public string Name { get; set; }
    public string Path { get; set; }
    public string Url { get; set; }

    public S3ObjectDto()
    {
        Name = string.Empty;
        Path = string.Empty;
        Url = string.Empty;
    }

    public S3ObjectDto(string name, string path, string url)
    {
        Name = name;
        Path = path;
        Url = url;
    }
}
