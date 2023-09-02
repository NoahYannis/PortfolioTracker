using PortfolioTrackerShared.Models.UserModels;
namespace PortfolioTrackerClient.Services.SettingsService

{
	public interface ISettingsService
	{
		Task<UserSettings> GetUserSettings(User user);
		Task<ServiceResponse<bool>> UpdateUserSettings(User user, UserSettings settings);
		Task<ServiceResponse<bool>> ResetUserSettings(User user);
	}
}
