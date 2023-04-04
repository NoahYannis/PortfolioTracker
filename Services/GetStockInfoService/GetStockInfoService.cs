using Newtonsoft.Json;
using PortfolioTracker.Models;
using System.Net.Http.Json;

namespace PortfolioTracker.Services.GetStockInfoService
{
    public class GetStockInfoService : IGetStockInfoService
    {
        private readonly HttpClient _httpClient;
        public GetStockInfoService(HttpClient httpClient)
        {
            httpClient = _httpClient;
        }

        public Task<decimal> GetStockPE_Ratio(Stock stock)
        {
            throw new NotImplementedException();
        }

        public async Task<decimal> GetStockData(Stock stock)
        {
            string apiKey = "pRdp25A7QOwhpQPUVvwmvl9czlt_HgnM";
            string apiUrl = $"https://api.polygon.io/v1/last/stocks/{stock.TickerSymbol}?apiKey={apiKey}";

            using var httpClient = new HttpClient();
            {
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    var aktie = await httpClient.GetFromJsonAsync<Stock>(jsonResponse);
                    //dynamic stockData = JsonConvert.DeserializeObject(jsonResponse);
                    //decimal currentPrice = stockData.last.price;
                    Console.WriteLine($"Current price of {stock.TickerSymbol}: ${aktie?.Price}");
                }
                else
                {
                    Console.WriteLine($"Failed to retrieve stock data for {stock.TickerSymbol}");
                }
            }

            return 0;
        }
    }
}
