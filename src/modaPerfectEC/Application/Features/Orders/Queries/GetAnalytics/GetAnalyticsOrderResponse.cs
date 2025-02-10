using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Orders.Queries.GetAnalytics;
public class GetAnalyticsOrderResponse
{
    public int TotalOrder {  get; set; }
    public int TotalActiveUser { get; set; }
    public int TotalProduct { get; set; }
    public double MonthlyIncomeTL { get; set; }
    public double MonthlyIncomeUSD { get; set; }
}
