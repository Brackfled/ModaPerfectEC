using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos;
public class UserStateOperationClaimDto
{
    public UserState UserState { get; set; }
    public bool Success { get; set; }
}
