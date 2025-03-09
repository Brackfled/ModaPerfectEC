using Application.Services.BackgroundWorkers;
using Application.Services.ExchangeService;
using Application.Services.Products;
using Domain.Entities;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Adapters.BackgroundWorkers.Hangfire;
public class HangfireJobs : BackgroundWorkerBase
{
    private readonly IProductService _productService;
    private readonly ExchangeServiceBase _exchangeService;

    public HangfireJobs(IProductService productService, ExchangeServiceBase exchangeService)
    {
        _productService = productService;
        _exchangeService = exchangeService;
    }

    public override async Task UpdateAllProductPriceUsd()
    {
        double exchangedUSDToTRY = await _exchangeService.GetUsdExchangeRateAsync();

        ICollection<Product> products = await _productService.GetAllAsync();

        foreach (Product product in products) 
        {
            double priceUSD = product.Price / exchangedUSDToTRY;
            double flooredPriceUSD = Math.Floor(priceUSD * 100) / 100;

            product.PriceUSD = flooredPriceUSD;
            await _productService.UpdateAsync(product);
        }
    }
}
