namespace PortfolioTrackerShared.Models
{
    public class Portfolio
    {
        /// <summary>
        /// All stocks inside the user's portfolio
        /// </summary>
        public List<PortfolioStock> Positions { get; set; } = new();
        public List<Order> OrderHistory { get; set; } = new();
        public string Name { get; set; } = string.Empty;
        public decimal TotalValue { get; set; } = decimal.Zero;

        #region Portfolio Performance

        public decimal? RelativePerformance
        {
            get
            {
                return Positions.Any() ? Positions.Sum(stock => stock.RelativePerformance) : decimal.Zero;
            }
            set { }
        }

        public decimal? AbsolutePerformance
        {
            get
            {
                return Positions.Any() ? Positions.Sum(stock => stock.AbsolutePerformance) : decimal.Zero;
            }
            set { }
        }

        #endregion
    }
}
