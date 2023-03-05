using PortfolioTracker.Models;

namespace PortfolioTracker.Services.GetStockInfoService
{
    public interface IGetStockInfoService
    {
        Task<decimal> GetStockPrice(Stock stock);
        Task<decimal> GetStockPE_Ratio(Stock stock);
    }
}
