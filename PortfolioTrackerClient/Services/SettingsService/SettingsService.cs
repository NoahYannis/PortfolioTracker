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
		public async Task<UserSettings> GetSettings(User user)
		{
			var response = await _http.GetFromJsonAsync<ServiceResponse<UserSettings>>($"{serverBaseDomain}/api/settings/{user.UserId}");
			return response?.Data;
		}

		public async Task<ServiceResponse<bool>> SaveSettings(UserSettings settings)
		{
			var response = await _http.PostAsJsonAsync($"{serverBaseDomain}/api/settings/save", settings);
			return await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
		}
	}
}