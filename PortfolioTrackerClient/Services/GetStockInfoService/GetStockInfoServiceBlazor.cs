using Newtonsoft.Json;
using PortfolioTrackerShared.Models;
using PortfolioTrackerShared.Other;
using System.Net.Http.Json;

namespace PortfolioTrackerClient.Services.GetStockInfoService
{
    public class GetStockInfoServiceBlazor : IGetStockInfoService
    {
        private readonly HttpClient _httpClient;

        public GetStockInfoServiceBlazor(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public ApiQueryStock CurrentStock { get; set; } = new();

        public async Task<ApiQueryStock> GetStockData(string tickerSymbol)
        {
            if (!string.IsNullOrWhiteSpace(tickerSymbol))
            {
                var response = await _httpClient.GetFromJsonAsync<ServiceResponse<ApiQueryStock>>($"https://localhost:7207/api/polygon/{tickerSymbol}");
                return CurrentStock = response.Data;
            }

            return new();
        }
    }
}
