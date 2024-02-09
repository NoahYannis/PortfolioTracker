namespace PortfolioTrackerClient.Services.SettingsService;


public interface ISettingsService
{
    Task<UserSettings> GetUserSettings(int userId);
    Task<bool> UpdateUserSettings(UserSettings settings);
    Task<ServiceResponse<bool>> ResetUserSettings(User user);
}
