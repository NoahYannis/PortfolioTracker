﻿using Newtonsoft.Json;
using PortfolioTrackerShared.Models;
using PortfolioTrackerShared.Other;

namespace PortfolioTrackerClient.Services.GetStockInfoService
{
    public class GetStockInfoServiceBlazor : IGetStockInfoService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        public ApiQueryStock CurrentStock { get; set; } = new ApiQueryStock { Ticker = "GOOGL" };
        public bool ApiCallSuccesful = true;
        private object httpClient;

        public GetStockInfoServiceBlazor(HttpClient httpClient, IConfiguration config)
        {
            httpClient = _httpClient;
            _config = config;
        }


        /// <summary>
        /// Retrieves stock data for the specified ticker symbol from an API.
        /// </summary>
        /// <param name="tickerSymbol"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<ApiQueryStock>> GetStockData(string tickerSymbol)
        {
            AppConfig appConfig = _config.GetSection("AppSettings").Get<AppConfig>(); // Returns the API Key from the appsettings.json file
            string apiKey = appConfig?.ApiKey;

            string date = DateTime.Now.AddHours(-24).ToString("yyyy-MM-dd");  // The free API version only delivers end of day data. A 24h delay is required.

            string url = $"https://api.polygon.io/v1/open-close/{tickerSymbol}/{date}?adjusted=true&apiKey={apiKey}";

            HttpClient client = new HttpClient();
            HttpResponseMessage httpResponse = await client.GetAsync(url);

            var serviceResponse = new ServiceResponse<ApiQueryStock>();

            if (httpResponse.IsSuccessStatusCode)
            {
                string json = await httpResponse.Content.ReadAsStringAsync();
                serviceResponse.Data = CurrentStock = JsonConvert.DeserializeObject<ApiQueryStock>(json);

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