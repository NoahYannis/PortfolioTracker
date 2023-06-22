using Newtonsoft.Json;
using PortfolioTrackerShared.Models;
using PortfolioTrackerShared.Other;
using PortfolioTrackerServer.Data;

namespace PortfolioTrackerServer.Services.GetStockInfoService
{
    public class GetStockInfoServiceBlazor : IGetStockInfoService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        public Stock CurrentStock { get; set; } = new Stock { Ticker = "GOOGL" };
        public bool ApiCallSuccesful = true;
        private object httpClient;

        public GetStockInfoServiceBlazor(HttpClient httpClient, IConfiguration config)
        {
            httpClient = _httpClient;
            _config = config;
        }


        /// <summary>
        /// Sends an API request with the specified stock ticker and returns its result 
        /// </summary>
        /// <param name="tickerSymbol"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<Stock>> GetStockData(string tickerSymbol)
        {
            AppConfig appConfig = _config.GetSection("AppSettings").Get<AppConfig>(); // Returns the API Key from the appsettings.json file
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
