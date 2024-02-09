using System.ComponentModel.DataAnnotations;

namespace PortfolioTrackerShared.Models.UserModels;

public class UserRegister
{

    [Required(ErrorMessage = "Please enter an email."), EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter a name."), StringLength(50, MinimumLength = 2)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter a password."), StringLength(100, MinimumLength = 1)]
    public string Password { get; set; } = string.Empty;

    [Compare("Password", ErrorMessage = "The passwords don't match.")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
