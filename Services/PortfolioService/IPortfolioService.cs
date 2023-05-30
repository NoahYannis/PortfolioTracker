using PortfolioTracker.Other;

namespace PortfolioTracker.Services.PortfolioService
{
    /// <summary>
    /// Portfolio functionalities
    /// </summary>
    public interface IPortfolioService
    {
        List<Stock> PortfolioStocks { get; set; }
        Task GetStocks();
        Task GetStock(string ticker);
        Task AddStock(Stock stock);
        Task DeleteStock(string ticker);
        Task UpdateStock(Stock stock);

    }
}
