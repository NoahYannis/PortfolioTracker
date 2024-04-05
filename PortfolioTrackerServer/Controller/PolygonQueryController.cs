using Microsoft.AspNetCore.Mvc;
using PortfolioTrackerServer.Services.GetStockInfoService;
using PortfolioTrackerShared.Models;
using PortfolioTrackerShared.Other;

namespace PortfolioTrackerServer.Controller;

[Route("api/polygon")]
[ApiController]
public class PolygonQueryController(IGetStockInfoService stockInfoService, IFetchAndUpdateStockPriceService fetchAndUpdate) : ControllerBase
{
    private readonly IGetStockInfoService _stockInfoService = stockInfoService;
    private readonly IFetchAndUpdateStockPriceService _fetchAndUpdate = fetchAndUpdate;

    [HttpGet("{tickerSymbol}")]
    public async Task<ActionResult<ServiceResponse<ApiQueryStock>>> GetStockData(string tickerSymbol)
    {
        var result = await _stockInfoService.GetStockData(tickerSymbol);
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<ApiQueryStock>>>> GetAllStockData(int userId)
    {
        var result = await _fetchAndUpdate.FetchCurrentStockPrices(userId);
        return Ok(result);
    }


}
