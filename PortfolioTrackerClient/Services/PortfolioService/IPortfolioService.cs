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

        /// <summary>
        /// Gets all stocks inside a user's portfolio
        /// </summary>
        /// <returns></returns>
        Task<List<PortfolioStock>> GetPortfolioStocks(int userId);
        Task<PortfolioStock> GetDatabaseStock(string ticker);
        Task<PortfolioStock> AddStock(PortfolioStock stockToAdd);
        Task<ServiceResponse<PortfolioStock>> UpdateStock(PortfolioStock stockToUpdate);
        Task<bool> DeleteStock(string ticker);

        /// <summary>
        /// Fetches Database stocks and sets PortfolioStocks on startup
        /// </summary>
        /// <returns></returns>
        Task InitializePortfolioAsync(int userId);

        /// <summary>
        /// Fetches and updates the current share price and position size of all portfolio stocks
        /// </summary>
        /// <returns></returns>
        Task<bool> UpdatePriceAndPositionSize();


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
