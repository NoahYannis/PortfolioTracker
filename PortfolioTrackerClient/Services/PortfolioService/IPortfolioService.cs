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
        Task<List<PortfolioStock>> GetDatabaseStocks();
        Task<PortfolioStock> GetDatabaseStock(string ticker);
        Task<PortfolioStock> AddStock(PortfolioStock stockToAdd);
        Task<ServiceResponse<PortfolioStock>> UpdateStock(PortfolioStock stockToUpdate);
        Task<bool> DeleteStock(string ticker);
        Task InitializeAsync();

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
