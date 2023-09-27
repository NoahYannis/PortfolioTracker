using PortfolioTrackerServer.Data;
using PortfolioTrackerServer.Services.GetStockInfoService;
using PortfolioTrackerServer.Services.PortfolioService;
using PortfolioTrackerShared.Models;
using PortfolioTrackerShared.Other;

namespace PortfolioTrackerServer.Services.FetchAndUpdateStockPriceService
{
    public class FetchAndUpdateStockPriceService : IFetchAndUpdateStockPriceService
    {
        private readonly DataContext _dataContext;
        private readonly IPortfolioService _portfolioService;
        private readonly IGetStockInfoService _getStockInfoService;

        public FetchAndUpdateStockPriceService(DataContext datacontext, IPortfolioService portfolioService, IGetStockInfoService getStockInfoService)
        {
            _dataContext = datacontext;
            _portfolioService = portfolioService;
            _getStockInfoService = getStockInfoService;
        }

        public async Task<ServiceResponse<List<ApiQueryStock>>> FetchCurrentStockPrices()
        {
            var portfolioStocks = _portfolioService.PortfolioStocks;

            if (portfolioStocks.Count is 0)
                return new ServiceResponse<List<ApiQueryStock>>() { Data = null, Success = false, Message = "Couldn't fetch database stocks" };

            var serviceResponse = new ServiceResponse<List<ApiQueryStock>>() { Data = new List<ApiQueryStock>() };

            foreach (PortfolioStock stock in portfolioStocks)
            {
                var response = await _getStockInfoService.GetStockData(stock?.Ticker);

                if (response.Success)
                {
                    serviceResponse.Data.Add(response.Data);
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = response.Message;
                }
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<PortfolioStock>>> UpdatePriceAndPositionSize()
        {
            var result = await FetchCurrentStockPrices();

            if (result.Success)
            {
                // Update share price
                _portfolioService.PortfolioStocks.ForEach(s => s.CurrentPrice = result.Data?[_portfolioService.PortfolioStocks.IndexOf(s)].Close);

                // Update position size
                _portfolioService.PortfolioStocks.ForEach(s => s.PositionSize = s.CurrentPrice * s.SharesOwned);

                await  _dataContext.SaveChangesAsync();

                return new ServiceResponse<List<PortfolioStock>> { Data = _portfolioService.PortfolioStocks };
            }

            return new ServiceResponse<List<PortfolioStock>> { Success = false, Message = result.Message };
        }

    }
}
