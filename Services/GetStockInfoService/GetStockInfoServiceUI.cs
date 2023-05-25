﻿using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PortfolioTracker.Other;
using System.Net.Http;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PortfolioTracker.Shared;

namespace PortfolioTracker.Services.GetStockInfoService
{
    public class GetStockInfoServiceUI : IGetStockInfoService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        public Stock CurrentStock { get; set; } = new Stock { Ticker = "GOOGL" };
        public bool ApiCallSuccesful = true;
        private object httpClient;

        public GetStockInfoServiceUI(HttpClient httpClient, IConfiguration config)
        {
            httpClient = _httpClient;
            _config = config;
        }

        public async Task<ServiceResponse<Stock>> GetStockData(string tickerSymbol)
        {
            AppConfig appConfig = _config.GetSection("AppSettings").Get<AppConfig>();
            string apiKey = appConfig?.ApiKey;
            string date = DateTime.Now.AddHours(-24).ToString("yyyy-MM-dd");  // The free API version only delivers end of day data. A 24h delay is required.

            string url = $"https://api.polygon.io/v1/open-close/{tickerSymbol}/{date}?adjusted=true&apiKey={apiKey}";

            HttpClient client = new HttpClient();
            HttpResponseMessage httpResponse = await client.GetAsync(url);

            var serviceResponse = new ServiceResponse<Stock>();

            if (httpResponse.IsSuccessStatusCode)
            {
                string json = await httpResponse.Content.ReadAsStringAsync();
                serviceResponse.Data = CurrentStock = JsonConvert.DeserializeObject<Stock>(json);

                if (serviceResponse.Data != null)
                {
                    serviceResponse.Data.Ticker = tickerSymbol;

                }              
                
                return serviceResponse;
            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = httpResponse.ReasonPhrase;
                return serviceResponse;
            }
        }
    }
}
