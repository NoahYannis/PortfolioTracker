using PortfolioTrackerShared.Models.UserModels;
using PortfolioTrackerShared.Other;

namespace PortfolioTrackerServer.Services.SettingsService
{
	public interface ISettingsService
	{
		Task<ServiceResponse<UserSettings>> GetUserSettings(User user);
		Task<bool> ResetUserSettings(User user);
		Task<bool> UpdateUserSettings(User user, UserSettings newSettings);

	}
}
