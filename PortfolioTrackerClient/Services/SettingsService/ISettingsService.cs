using PortfolioTrackerShared.Models.UserModels;
namespace PortfolioTrackerClient.Services.SettingsService

{
	public interface ISettingsService
	{
		Task<ServiceResponse<bool>> SaveSettings(UserSettings settings);
		Task<UserSettings> GetSettings(User user);
	}
}
