using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.BackgroundWorkers;
public abstract class BackgroundWorkerBase
{
    public abstract Task UpdateAllProductPriceUsd();
}
