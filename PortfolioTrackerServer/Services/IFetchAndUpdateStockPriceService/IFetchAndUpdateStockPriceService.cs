using PortfolioTrackerShared.Models;
using PortfolioTrackerShared.Other;

namespace PortfolioTrackerServer.Services.GetStockInfoService;


/// <summary>
/// Functionalities to fetch current stock price from API and update PortfolioService.PortfolioStocks
/// </summary>
public interface IFetchAndUpdateStockPriceService
{
    Task<ServiceResponse<List<ApiQueryStock>>> FetchCurrentStockPrices(int userId);
    Task<ServiceResponse<List<PortfolioStock>>> UpdatePriceAndPositionSize(int userId);

}
