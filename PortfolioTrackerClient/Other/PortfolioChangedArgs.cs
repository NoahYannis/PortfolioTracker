using PortfolioTrackerShared.Models;

namespace PortfolioTrackerShared.Other
{
    public enum PortfolioAction
    {
        Added,
        Modified,
        Deleted
    }

    /// <summary>
    /// Represents the event arguments for a portfolio change event.
    /// </summary>
    public class PortfolioChangedArgs : EventArgs
    {
        public List<Stock> UpdatedPortfolio { get; set; }
        public Stock ModifiedStock { get; set; }
        public PortfolioAction PortfolioAction { get; set; }

        public PortfolioChangedArgs(List<Stock> updatedPortfolio, Stock? modifiedStock = null, PortfolioAction portfolioAction = 0)
        {
            UpdatedPortfolio = updatedPortfolio;
            ModifiedStock = modifiedStock;
            PortfolioAction = portfolioAction;
        }
    }
}