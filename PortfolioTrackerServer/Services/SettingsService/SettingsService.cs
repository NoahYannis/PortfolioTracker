using Microsoft.EntityFrameworkCore;
using PortfolioTrackerServer.Data;
using PortfolioTrackerShared.Models.UserModels;
using PortfolioTrackerShared.Other;

namespace PortfolioTrackerServer.Services.SettingsService
{
	public class SettingsService : ISettingsService
	{
		private readonly DataContext _dataContext;

		public SettingsService(DataContext dataContext)
		{
			_dataContext = dataContext;
		}

		/// <summary>
		/// Return settings for a given user.
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		public async Task<ServiceResponse<UserSettings>> GetUserSettings(User user)
		{
			// User whose settings we want
			var userSettings = await _dataContext.UserSettings.FirstOrDefaultAsync(us => us.UserId == user.UserId);

			// If found return the corresponding user`s settings.
			return userSettings is not null ? new ServiceResponse<UserSettings> { Data = userSettings }
				: new ServiceResponse<UserSettings>
				{
					Success = false,
					Message = $"No settings found for User '{user.UserName}'"
				};
		}

		/// <summary>
		/// Updates a user's settings.
		/// </summary>
		/// <param name="user"></param>
		public async Task<bool> UpdateUserSettings(UserSettings updatedSettings)
		{
			//var existingUser = await _dataContext.Users.FirstOrDefaultAsync(us => us.UserId == updatedSettings.UserId);
			//var existingSettings = await _dataContext.UserSettings.FirstOrDefaultAsync(us => us.UserId == existingUser.UserId);

			var existingUser = await _dataContext.Users.FirstOrDefaultAsync(us => us.UserId == updatedSettings.UserId);

			if (existingUser is null)
				return false;

			//_dataContext.UserSettings.Remove(existingSettings);
			//_dataContext.UserSettings.Add(updatedSettings)

			existingUser.Settings = updatedSettings;
			_dataContext.Users.Update(existingUser);
			_dataContext.UserSettings.Update(existingUser.Settings);

			await _dataContext.SaveChangesAsync(); // Save the changes to the database

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
}
