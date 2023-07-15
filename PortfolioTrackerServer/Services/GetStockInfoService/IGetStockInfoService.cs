using Microsoft.AspNetCore.Mvc;
using PortfolioTrackerShared.Models;
using PortfolioTrackerShared.Other;

namespace PortfolioTrackerServer.Services.GetStockInfoService
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


        /// <summary>
        /// Stockdata for all stocks in side a portfolio
        /// </summary>
        /// <returns></returns>
        Task<ServiceResponse<List<ApiQueryStock>>> FetchCurrentStockPrices();


        /// <summary>
        /// The result of the API call
        /// </summary>
        public ApiQueryStock CurrentStock { get; set; }
    }
}
