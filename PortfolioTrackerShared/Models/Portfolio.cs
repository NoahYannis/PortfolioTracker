using System.ComponentModel.DataAnnotations;
using PortfolioTrackerShared.Models.UserModels;

namespace PortfolioTrackerShared.Models
{
    public class Portfolio
    {
        /// <summary>
        /// All stocks inside the user's portfolio
        /// </summary>
        /// 

        [Key]
        public int Id { get; set; }
        public List<PortfolioStock> Positions { get; set; } = new();
        public List<Order> OrderHistory { get; set; } = new();

        /// <summary>
        /// Portfolio Owner
        /// </summary>
        /// 
        public User User { get; set; } = new();


        /// <summary>
        /// Portfolio Name
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Total value ($)
        /// </summary>
        public decimal Value { get; set; } = decimal.Zero;
        public decimal TotalRelativePerfomance { get; set; } = decimal.Zero;
        public decimal TotalAbsolutePerformance { get; set; } = decimal.Zero;

    }
}
