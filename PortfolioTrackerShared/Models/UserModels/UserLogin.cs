using System.ComponentModel.DataAnnotations;

namespace PortfolioTrackerShared.Models.UserModels;

public class UserLogin
{
    [Required(ErrorMessage = "Please enter an email.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter a password.")]
    public string Password { get; set; } = string.Empty;
}