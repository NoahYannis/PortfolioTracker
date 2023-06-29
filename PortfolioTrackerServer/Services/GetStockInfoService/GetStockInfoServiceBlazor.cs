using Newtonsoft.Json;
using PortfolioTrackerClient;
using PortfolioTrackerShared.Models;
using PortfolioTrackerShared.Other;

namespace PortfolioTrackerServer.Services.GetStockInfoService
{
    public class GetStockInfoServiceBlazor : IGetStockInfoService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        public ApiQueryStock CurrentStock { get; set; } = new();

        public GetStockInfoServiceBlazor(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }


        /// <summary>
        /// Sends an API request with the specified stock ticker and returns its result 
        /// </summary>
        /// <param name="tickerSymbol"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<ApiQueryStock>> GetStockData(string tickerSymbol)
        {
            AppConfig appConfig = _config.GetSection("AppSettings").Get<AppConfig>();  // Returns the API Key from the appsettings.json file
            string apiKey = appConfig?.ApiKey;

            string date = DateTime.Now.AddHours(-24).ToString("yyyy-MM-dd");  // The free API version only delivers end of day data. A 24h delay is required.
            string url = $"https://api.polygon.io/v1/open-close/{tickerSymbol}/{date}?adjusted=true&apiKey={apiKey}";

            HttpResponseMessage httpResponse = await _httpClient.GetAsync(url);

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
