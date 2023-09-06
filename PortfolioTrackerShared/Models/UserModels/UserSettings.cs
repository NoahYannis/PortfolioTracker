using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
		public string ColorScheme { get; set; } = "#551A8B"; // Default value purple

		/// <summary>
		/// Investing goals (portfolio value, dividend income per month...)
		/// </summary>
		public string InvestingGoals { get; set; } = string.Empty;

		public string PreferedLanguage { get; set; } = "en";
	}
}
