using System.ComponentModel.DataAnnotations;

namespace PortfolioTrackerShared.Models.UserModels
{
    public class UserRegister
    {

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, StringLength(50, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(100, MinimumLength = 1)]
        public string Password { get; set; } = string.Empty;

        [Compare("Password", ErrorMessage = "The passwords don't match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
