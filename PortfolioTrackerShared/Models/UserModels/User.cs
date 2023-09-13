using System.ComponentModel.DataAnnotations;

namespace PortfolioTrackerShared.Models.UserModels
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "User name required."), StringLength(50, ErrorMessage = "User name must be between 1 and 50 characters")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email required."), StringLength(100, ErrorMessage = "Email must not exceed 100 characters")]
        public string Email { get; set; } = string.Empty;

        public UserSettings Settings { get; set; } = new();
        public Portfolio Portfolio { get; set; } = new();

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
