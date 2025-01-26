using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ExchangeService;
public abstract class ExchangeServiceBase
{
    public abstract Task<double> GetUsdExchangeRateAsync();
}
