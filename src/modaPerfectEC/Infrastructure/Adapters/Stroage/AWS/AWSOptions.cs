using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Adapters.Stroage.AWS;
public class AWSOptions
{
    public string AccessKey { get; set; }
    public string SecretKey { get; set; }
    public string Region { get; set; }

    public AWSOptions()
    {
        AccessKey = string.Empty;
        SecretKey = string.Empty;
        Region = string.Empty;
    }

    public AWSOptions(string accessKey, string secretKey, string region)
    {
        AccessKey = accessKey;
        SecretKey = secretKey;
        Region = region;
    }
}
