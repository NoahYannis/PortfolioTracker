using PortfolioTrackerClient.Pages;
using PortfolioTrackerShared.Models;
using PortfolioTrackerShared.Models.UserModels;
using System.Net.Http.Json;

namespace PortfolioTrackerClient.Services.SettingsService
{
	public class SettingsService : ISettingsService
	{
		private readonly HttpClient _http;
		private string serverBaseDomain = "https://localhost:7207";


		public SettingsService(HttpClient http)
		{
			_http = http;
		}
		public async Task<UserSettings> GetUserSettings(User user)
		{
			var response = await _http.GetFromJsonAsync<ServiceResponse<UserSettings>>($"{serverBaseDomain}/api/settings/{user.UserId}");
			return response?.Data;
		}
        public async Task<ServiceResponse<bool>> UpdateUserSettings(User user, UserSettings settings)
        {
            var response = await _http.PutAsJsonAsync($"{serverBaseDomain}/api/settings/save", new { user, settings });
            return await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
        }

		public async Task<ServiceResponse<bool>> ResetUserSettings(User user)
        {
            var response = await _http.PutAsJsonAsync($"{serverBaseDomain}/api/settings/reset", user);
            return await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
        }
    }
}