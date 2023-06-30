using Newtonsoft.Json;
using PortfolioTrackerShared.Models;
using PortfolioTrackerShared.Other;
using System.Net.Http.Json;

namespace PortfolioTrackerClient.Services.GetStockInfoService
{
    public class GetStockInfoServiceBlazor : IGetStockInfoService
    {
        private readonly HttpClient _httpClient;
        public ApiQueryStock CurrentStock { get; set; } = new();

        public GetStockInfoServiceBlazor(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiQueryStock> GetStockData(string tickerSymbol)
        {
            var response = await _httpClient.GetAsync($"api/polygon/{tickerSymbol}");
            return CurrentStock = (await response.Content.ReadFromJsonAsync<ServiceResponse<ApiQueryStock>>()).Data;
        }
    }
}
