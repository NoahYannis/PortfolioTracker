using PortfolioTrackerShared.Models;
using PortfolioTrackerShared.Other;

namespace PortfolioTrackerClient.Services.GetStockInfoService
{
    /// <summary>
    /// An interface to fetch data from a stock API
    /// </summary>
    public interface IGetStockInfoService
    {
        /// <summary>
        /// Executes an API call and retrieves data from it
        /// </summary>
        /// <param name="tickerSymbol"></param>
        /// <returns></returns>
        Task<ServiceResponse<Stock>> GetStockData(string tickerSymbol);


        /// <summary>
        /// The result of the API call
        /// </summary>
        public Stock CurrentStock { get; set; }
    }
}
