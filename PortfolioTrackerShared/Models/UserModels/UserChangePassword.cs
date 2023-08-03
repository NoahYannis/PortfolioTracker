using System.ComponentModel.DataAnnotations;

namespace PortfolioTrackerShared.Models.UserModels
{
    public class UserChangePassword
    {
        [Required, StringLength(100, MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;

        [Compare("Password", ErrorMessage = "The passwords don't match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
