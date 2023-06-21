using PortfolioTrackerShared.Models;

namespace PortfolioTrackerShared.Other
{
    public enum PortfolioAction
    {
        Added,
        Modified,
        Deleted
    }

    public class PortfolioChangedArgs : EventArgs
    {
        public List<Stock> UpdatedPortfolio { get; set; }
        public Stock DeletedStock { get; set; }
        public PortfolioAction PortfolioAction { get; set; }

        public PortfolioChangedArgs(List<Stock> updatedPortfolio, Stock? deletedStock = null)
        {
            UpdatedPortfolio = updatedPortfolio;
            DeletedStock = deletedStock;
        }

    }
}