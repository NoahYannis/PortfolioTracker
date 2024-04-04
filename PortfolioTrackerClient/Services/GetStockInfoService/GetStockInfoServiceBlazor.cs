using PortfolioTrackerShared.Models;
using System.Net.Http.Json;

namespace PortfolioTrackerClient.Services.GetStockInfoService;

public class GetStockInfoServiceBlazor(HttpClient httpClient) : IGetStockInfoService
{
    private readonly HttpClient _httpClient = httpClient;

    private readonly string _server = "https://localhost:7207";

    public ApiQueryStock CurrentStock { get; set; } = new();

    /// <summary>
    /// Sends an HTTP request to the server with the specified ticker
    /// </summary>
    /// <param name="tickerSymbol"></param>
    /// <returns></returns>
    public async Task<ServiceResponse<ApiQueryStock>> GetStockData(string tickerSymbol)
    {
        return await _httpClient.GetFromJsonAsync<ServiceResponse<ApiQueryStock>>($"{_server}/api/polygon/{tickerSymbol}") ?? new();
    }

    public async Task<ServiceResponse<List<ApiQueryStock>>> GetAllStockData(int userId)
    {
        return await _httpClient.GetFromJsonAsync<ServiceResponse<List<ApiQueryStock>>>($"{_server}/api/polygon?userId={userId}") ?? new();
    }
}
