using PortfolioTrackerShared.Models;
using PortfolioTrackerShared.Other;

namespace PortfolioTrackerServer.Services.PortfolioService;

/// <summary>
/// Portfolio functionalities
/// </summary>
public interface IPortfolioService
{
    #region Database Stocks 

    Task<ServiceResponse<List<PortfolioStock>>> GetPortfolioStocks(int userId);
    Task<ServiceResponse<PortfolioStock>> GetStock(string ticker, int userId);
    Task<ServiceResponse<bool>> DeleteStock(string stockToDelete, int userId);
    Task<ServiceResponse<PortfolioStock>> AddStock(PortfolioStock stock, int userId);
    Task<ServiceResponse<PortfolioStock>> UpdateStock(PortfolioStock stock, int userId);

    //event EventHandler<PortfolioChangedArgs> PortfolioChanged;
    //void OnPortfolioChanged(List<PortfolioStock> portfolioStocks, PortfolioStock? deletedStock = null);


    #endregion

}
