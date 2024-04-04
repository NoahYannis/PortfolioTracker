using System.Net.Http.Json;

namespace PortfolioTrackerClient.Services.EmailService;

public class EmailService(HttpClient http) : IEmailService
{
    private readonly HttpClient _http = http;
    private readonly string _serverBaseDomain = "https://localhost:7207";

    public async Task<bool> SendEmail(string body, string recipientAddress)
    {
        var response = await _http.PostAsJsonAsync($"{_serverBaseDomain}/api/email?recipientAddress={recipientAddress}", body);
        return await response.Content.ReadFromJsonAsync<bool>();
    }
}
