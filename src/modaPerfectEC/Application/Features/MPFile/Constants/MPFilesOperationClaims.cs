using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.MPFile.Constants;
public class MPFilesOperationClaims
{
    private const string _section = "MPFiles";

    public const string Create = $"{_section}.Create";
    public const string Deleted = $"{_section}.Delete";
    public const string Read = $"{_section}.Read";
}
