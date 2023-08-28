using System;
using System.Collections.Generic;
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
		public Color ColorScheme { get; set; } = Color.Purple; // Todo: Implement color wheel

		/// <summary>
		/// Investing goals (portfolio value, dividend income per month...)
		/// </summary>
		public List<string> InvestingGoals { get; set; } = new List<string>();

		public string PreferedLanguage { get; set; } = "en";
	}
}
