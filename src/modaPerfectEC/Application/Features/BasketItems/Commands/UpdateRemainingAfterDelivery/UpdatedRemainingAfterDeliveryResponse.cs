using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.BasketItems.Commands.UpdateRemainingAfterDelivery;
public class UpdatedRemainingAfterDeliveryResponse
{
    public Guid Id { get; set; }
    public int ProductAmount {  get; set; }
    public int RemainingAfterDelivery { get; set; }
 
}
