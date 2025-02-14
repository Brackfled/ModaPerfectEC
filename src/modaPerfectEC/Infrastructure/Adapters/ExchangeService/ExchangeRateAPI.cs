﻿using Application.Services.ExchangeService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Adapters.ExchangeService;
public class ExchangeRateAPI : ExchangeServiceBase
{
    private readonly HttpClient _httpClient;

    public ExchangeRateAPI(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public override async Task<double> GetUsdExchangeRateAsync()
    {
        using (var webClient = new System.Net.WebClient())
        {
            var json = webClient.DownloadString("https://v6.exchangerate-api.com/v6/c652ecbee8e50248394807f8/latest/USD");
            API_Obj data = JsonConvert.DeserializeObject<API_Obj>(json);
            return data.conversion_rates.TRY;
        }
    }
}


public class API_Obj
{
    public string result { get; set; }
    public string documentation { get; set; }
    public string terms_of_use { get; set; }
    public string time_last_update_unix { get; set; }
    public string time_last_update_utc { get; set; }
    public string time_next_update_unix { get; set; }
    public string time_next_update_utc { get; set; }
    public string base_code { get; set; }
    public ConversionRate conversion_rates { get; set; }
}

public class ConversionRate
{
    public double AED { get; set; }
    public double ARS { get; set; }
    public double AUD { get; set; }
    public double BGN { get; set; }
    public double BRL { get; set; }
    public double BSD { get; set; }
    public double CAD { get; set; }
    public double CHF { get; set; }
    public double CLP { get; set; }
    public double CNY { get; set; }
    public double COP { get; set; }
    public double CZK { get; set; }
    public double DKK { get; set; }
    public double DOP { get; set; }
    public double EGP { get; set; }
    public double EUR { get; set; }
    public double FJD { get; set; }
    public double GBP { get; set; }
    public double GTQ { get; set; }
    public double HKD { get; set; }
    public double HRK { get; set; }
    public double HUF { get; set; }
    public double IDR { get; set; }
    public double ILS { get; set; }
    public double INR { get; set; }
    public double ISK { get; set; }
    public double JPY { get; set; }
    public double KRW { get; set; }
    public double KZT { get; set; }
    public double MXN { get; set; }
    public double MYR { get; set; }
    public double NOK { get; set; }
    public double NZD { get; set; }
    public double PAB { get; set; }
    public double PEN { get; set; }
    public double PHP { get; set; }
    public double PKR { get; set; }
    public double PLN { get; set; }
    public double PYG { get; set; }
    public double RON { get; set; }
    public double RUB { get; set; }
    public double SAR { get; set; }
    public double SEK { get; set; }
    public double SGD { get; set; }
    public double THB { get; set; }
    public double TRY { get; set; }
    public double TWD { get; set; }
    public double UAH { get; set; }
    public double USD { get; set; }
    public double UYU { get; set; }
    public double ZAR { get; set; }
}
