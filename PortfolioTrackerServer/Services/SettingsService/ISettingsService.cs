using PortfolioTrackerShared.Models.UserModels;
using PortfolioTrackerShared.Other;

namespace PortfolioTrackerServer.Services.SettingsService;

public interface ISettingsService
{
    Task<ServiceResponse<UserSettings>> GetUserSettings(int userId);
    Task<bool> ResetUserSettings(User user);
    Task<bool> UpdateUserSettings(UserSettings newSettings);

}
