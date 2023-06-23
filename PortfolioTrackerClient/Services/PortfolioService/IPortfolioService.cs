using PortfolioTrackerShared.Models;
using PortfolioTrackerShared.Other;

namespace PortfolioTrackerClient.Services.PortfolioService
{
    /// <summary>
    /// Portfolio functionalities
    /// </summary>
    public interface IPortfolioService
    {
        #region Stocks 

        List<PortfolioStock> PortfolioStocks { get; set; }
        Task<List<PortfolioStock>> GetStocks();
        Task<PortfolioStock> GetStock(string ticker);
        Task<bool> AddStock(PortfolioStock stock);
        Task<bool> DeleteStock(string ticker);
        Task<bool> UpdateStock(PortfolioStock stock);

        event EventHandler<PortfolioChangedArgs> PortfolioChanged;
        void OnPortfolioChanged(List<PortfolioStock> portfolioStocks, PortfolioStock? modifiedStock = null, PortfolioAction portfolioAction = 0);


        #endregion

        #region Orders

        List<Order> Orders { get; set; }
        Task<List<Order>> GetOrders();
        Task<Order> GetOrder(int orderNumber);
        Task UpdateOrder(Order order);
        Task DeleteOrder(int orderNumber);

        
        #endregion


    }
}
