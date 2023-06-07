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
        public List<Stock> UpdatedPortfolio { get; }
        public PortfolioAction PortfolioAction { get; set; }

        public PortfolioChangedArgs(List<Stock> updatedPortfolio)
        {
            UpdatedPortfolio = updatedPortfolio;
        }

        public event EventHandler<PortfolioChangedArgs> PortfolioChanged;

        public delegate void EventHandler<TEventArgs>(object sender, TEventArgs e) where TEventArgs : EventArgs;


    }
}