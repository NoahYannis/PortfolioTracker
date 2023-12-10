using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortfolioTrackerShared.Models.UserModels
{
    /// <summary>
    /// Settings defined by the user
    /// </summary>
    public class UserSettings
	{
		[Key]
		public int Id { get; set; }

		[ForeignKey("UserId")]
		public int UserId { get; set; }
		public string ColorScheme { get; set; } = "#013220";

		/// <summary>
		/// Investing goals (portfolio value, dividend income per month...)
		/// </summary>
		public string InvestingGoals { get; set; } = string.Empty;

	}
}
