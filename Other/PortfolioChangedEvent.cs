using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PortfolioTracker.Other
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
        public PortfolioAction PortfolioAction { get; set; }

        public PortfolioChangedArgs(List<Stock> updatedPortfolio)
        {
            UpdatedPortfolio = updatedPortfolio;
        }

    }
}