using PortfolioTrackerShared.Models;
using System.Net.Http.Json;

namespace PortfolioTrackerClient.Services.GetStockInfoService;

public class GetStockInfoServiceBlazor : IGetStockInfoService
{
    private readonly HttpClient _httpClient;

    public GetStockInfoServiceBlazor(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public ApiQueryStock CurrentStock { get; set; } = new();

    /// <summary>
    /// Sends an HTTP request to the server with the specified ticker
    /// </summary>
    /// <param name="tickerSymbol"></param>
    /// <returns></returns>
    public async Task<ServiceResponse<ApiQueryStock>> GetStockData(string tickerSymbol)
    {
        return await _httpClient.GetFromJsonAsync<ServiceResponse<ApiQueryStock>>($"https://localhost:7207/api/polygon/{tickerSymbol}");
    }

    public async Task<ServiceResponse<List<ApiQueryStock>>> GetAllStockData(int userId)
    {
        return await _httpClient.GetFromJsonAsync<ServiceResponse<List<ApiQueryStock>>>($"https://localhost:7207/api/polygon?userId={userId}");
    }
}
