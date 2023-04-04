using PortfolioTracker.Models;

namespace PortfolioTracker.Services.GetStockInfoService
{
    public interface IGetStockInfoService
    {
        Task<decimal> GetStockData(Stock stock);
        Task<decimal> GetStockPE_Ratio(Stock stock);
    }
}
