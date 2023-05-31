using System.Transactions;

namespace PortfolioTracker.Models
{
    public class Portfolio
    {
        /// <summary>
        /// All stocks inside the user's portfolio
        /// </summary>
        public List<Stock> Positions { get; set; } = new();
        public List<Order> OrderHistory { get; set; } = new();
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Value { get; set; } = decimal.Zero;

        #region Portfolio Performance

        public decimal RelativePerformance
        {
            get { return GetRelativePerformance(); }
            set { }
        }

        public decimal AbsolutePerformance
        {
            get { return GetAbsolutePerformance(); }
            set { }
        }

        public decimal GetRelativePerformance()
        {
            if (Positions.Any())
            {
                foreach (Stock stock in Positions)
                {
                    RelativePerformance += stock.RelativePerformance;
                }

                return RelativePerformance;
            }

            return decimal.Zero;
        }
        public decimal GetAbsolutePerformance()
        {
            if (Positions.Any())
            {
                foreach (Stock stock in Positions)
                {
                    AbsolutePerformance += stock.AbsolutePerformance;
                }

                return AbsolutePerformance;
            }

            return decimal.Zero;
        }

        #endregion
    }
}
