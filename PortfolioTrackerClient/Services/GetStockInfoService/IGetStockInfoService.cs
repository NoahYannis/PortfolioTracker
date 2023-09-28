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
        Task<ServiceResponse<ApiQueryStock>> GetStockData(string tickerSymbol);
        Task<ServiceResponse<List<ApiQueryStock>>> GetAllStockData(int userId);


        /// <summary>
        /// The result of the API call
        /// </summary>
        public ApiQueryStock CurrentStock { get; set; }
    }
}
