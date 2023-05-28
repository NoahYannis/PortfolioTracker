namespace PortfolioTracker.Models
{
    public class Portfolio
    {
        /// <summary>
        /// All stocks inside the user's portfolio
        /// </summary>
        public List<Stock> Positions { get; set; } = new();

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Value { get; set; } = decimal.Zero;
        public decimal ProcentualPerformance { get; set; } = GetProcentualPerformance();
        public decimal AbsolutePerformance { get; set; } = GetAbsolutePerformance();

        // Transaction history to be implemented

        private static decimal GetProcentualPerformance()
        {
            throw new NotImplementedException();
        }
        private static decimal GetAbsolutePerformance()
        {
            throw new NotImplementedException();
        }
    }
}
