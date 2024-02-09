using PortfolioTrackerServer.Data;
using PortfolioTrackerServer.Services.GetStockInfoService;
using PortfolioTrackerServer.Services.PortfolioService;
using PortfolioTrackerShared.Models;
using PortfolioTrackerShared.Other;

namespace PortfolioTrackerServer.Services.FetchAndUpdateStockPriceService;

public class FetchAndUpdateStockPriceService : IFetchAndUpdateStockPriceService
{
    private readonly DataContext _dataContext;
    private readonly IPortfolioService _portfolioService;
    private readonly IGetStockInfoService _getStockInfoService;
    private List<PortfolioStock> PortfolioStocks = new List<PortfolioStock>();

    public FetchAndUpdateStockPriceService(DataContext datacontext, IPortfolioService portfolioService, IGetStockInfoService getStockInfoService)
    {
        _dataContext = datacontext;
        _portfolioService = portfolioService;
        _getStockInfoService = getStockInfoService;
    }

    public async Task<ServiceResponse<List<ApiQueryStock>>> FetchCurrentStockPrices(int userId)
    {
        var portfolioStocks = await _portfolioService.GetPortfolioStocks(userId);
        PortfolioStocks = portfolioStocks.Data;

        if (portfolioStocks.Data is null || portfolioStocks.Data.Count is 0)
            return new ServiceResponse<List<ApiQueryStock>>() { Data = null, Success = false, Message = "Couldn't fetch database stocks" };

        var serviceResponse = new ServiceResponse<List<ApiQueryStock>>() { Data = new() };

        foreach (PortfolioStock stock in PortfolioStocks)
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

    public async Task<ServiceResponse<List<PortfolioStock>>> UpdatePriceAndPositionSize(int userId)
    {
        var result = await FetchCurrentStockPrices(userId);

        if (result.Success)
        {
            // Update share price and position size
            PortfolioStocks.ForEach(s =>
            {
                s.CurrentPrice = result.Data?[PortfolioStocks.IndexOf(s)].Close ?? 0;
                s.PositionSize = s.CurrentPrice * s.SharesOwned;
            });


            await _dataContext.SaveChangesAsync();

            return new ServiceResponse<List<PortfolioStock>> { Data = PortfolioStocks };
        }

        return new ServiceResponse<List<PortfolioStock>> { Success = false, Message = result.Message };
    }

}
