using Microsoft.EntityFrameworkCore;
using PortfolioTrackerServer.Data;
using PortfolioTrackerShared.Models.UserModels;
using PortfolioTrackerShared.Other;

namespace PortfolioTrackerServer.Services.SettingsService;

public class SettingsService(DataContext dataContext) : ISettingsService
{
    private readonly DataContext _dataContext = dataContext;

    /// <summary>
    /// Return settings for a given user.
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public async Task<ServiceResponse<UserSettings>> GetUserSettings(int userId)
    {
        // User whose settings we want
        var userSettings = await _dataContext.UserSettings.FirstOrDefaultAsync(us => us.UserId == userId);

        // If found return the corresponding user`s settings.
        return userSettings is not null ? new ServiceResponse<UserSettings> { Data = userSettings }
            : new ServiceResponse<UserSettings>
            {
                Success = false,
                Message = "No settings found."
            };
    }

    /// <summary>
    /// Updates a user's settings.
    /// </summary>
    /// <param name="user"></param>
    public async Task<bool> UpdateUserSettings(UserSettings updatedSettings)
    {
        var existingUser = await _dataContext.Users.FirstOrDefaultAsync(us => us.UserId == updatedSettings.UserId);

        if (existingUser is null)
            return false;

        existingUser.Settings = updatedSettings;
        _dataContext.Users.Update(existingUser);
        _dataContext.UserSettings.Update(existingUser.Settings);

        await _dataContext.SaveChangesAsync(); // Save changes to the database

        return true;
    }

    /// <summary>
    /// Resets a user's settings to default values.
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public async Task<bool> ResetUserSettings(User user)
    {
        var existingSettings = await _dataContext.UserSettings.FirstOrDefaultAsync(us => us.UserId == user.UserId);

        if (existingSettings is null)
            return false;

        _dataContext.UserSettings.Remove(existingSettings);
        _dataContext.UserSettings.Add(new UserSettings { UserId = user.UserId });
        await _dataContext.SaveChangesAsync();

        return true;
    }

}
