using PortfolioTracker.Other;

namespace PortfolioTracker.Services.GetStockInfoService
{
    public interface IGetStockInfoService
    {
        Task<ServiceResponse<Stock>> GetStockData(string tickerSymbol);
        public Stock CurrentStock { get; set; }
    }
}
