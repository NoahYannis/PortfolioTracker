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
        private readonly SettingsService _settingsService;

        public SettingsController(SettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        [HttpGet]
        public async Task<ActionResult<UserSettings>> GetUserSettings(User user)
        {
            var result = await _settingsService.GetUserSettings(user);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<bool>>> UpdateUserSettings(UserSettings settings)
        {
            var result = await _settingsService.UpdateUserSettings(settings);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<bool>> ResetUserSettings(User user)
        {
            var result = await _settingsService.ResetUserSettings(user);
            return Ok(result);
        }

    }
}
