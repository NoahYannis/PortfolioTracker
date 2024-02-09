using System.Net.Http.Json;

namespace PortfolioTrackerClient.Services.SettingsService;

public class SettingsService : ISettingsService
{
    private string serverBaseDomain = "https://localhost:7207";

    private readonly HttpClient _http;

    public SettingsService(HttpClient http)
    {
        _http = http;
    }

    public async Task<UserSettings?> GetUserSettings(int userId)
    {
        var response = await _http.GetFromJsonAsync<ServiceResponse<UserSettings>>($"{serverBaseDomain}/api/settings?userId={userId}");
        return response?.Data;
    }

    public async Task<bool> UpdateUserSettings(UserSettings settings)
    {
        var response = await _http.PutAsJsonAsync($"{serverBaseDomain}/api/settings/save", settings);
        return await response.Content.ReadFromJsonAsync<bool>();
    }

    public async Task<ServiceResponse<bool>> ResetUserSettings(User user)
    {
        var response = await _http.PutAsJsonAsync($"{serverBaseDomain}/api/settings/reset", user);
        return await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
    }
}