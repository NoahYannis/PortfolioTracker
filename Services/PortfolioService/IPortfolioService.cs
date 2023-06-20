using PortfolioTracker.Models;
using PortfolioTracker.Other;

namespace PortfolioTracker.Services.PortfolioService
{
    /// <summary>
    /// Portfolio functionalities
    /// </summary>
    public interface IPortfolioService
    {
        #region Stocks 

        List<Stock> PortfolioStocks { get; set; }
        Task<List<Stock>> GetStocks();
        Task<Stock> GetStock(string ticker);
        Task<bool> AddStock(Stock stock);
        Task<bool> DeleteStock(string ticker);
        Task<bool> UpdateStock(Stock stock);

        event EventHandler<PortfolioChangedArgs> PortfolioChanged;
        void OnPortfolioChanged(List<Stock> portfolioStocks, Stock? deletedStock = null);


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
