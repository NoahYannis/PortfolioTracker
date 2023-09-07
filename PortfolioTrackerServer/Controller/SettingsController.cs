using Microsoft.AspNetCore.Mvc;
using PortfolioTrackerServer.Services.SettingsService;
using PortfolioTrackerShared.Models.UserModels;
using PortfolioTrackerShared.Other;

namespace PortfolioTrackerServer.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly ISettingsService _settingsService;

        public SettingsController(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        [HttpGet()] //TODO: adjust routing
        public async Task<ActionResult<UserSettings>> GetUserSettings(User user)
        {
            var result = await _settingsService.GetUserSettings(user);
            return Ok(result);
        }

        [HttpPut("save")]
        public async Task<ActionResult<bool>> UpdateUserSettings(UserSettings settings)
        {
            var result = await _settingsService.UpdateUserSettings(settings);
            return Ok(result);
        }

        [HttpPut("reset")]
        public async Task<ActionResult<bool>> ResetUserSettings(User user)
        {
            var result = await _settingsService.ResetUserSettings(user);
            return Ok(result);
        }

    }
}
