using Microsoft.AspNetCore.Mvc;
using PortfolioTrackerServer.Services.GetStockInfoService;
using PortfolioTrackerServer.Services.PortfolioService;
using PortfolioTrackerShared.Models;
using PortfolioTrackerShared.Other;

namespace PortfolioTrackerServer.Controller;

[ApiController]
[Route("api/portfolio")]
public class PortfolioController(IPortfolioService portfolioService, IFetchAndUpdateStockPriceService fetchAndUpdate) : ControllerBase
{
    private readonly IPortfolioService _portfolioService = portfolioService;
    private readonly IFetchAndUpdateStockPriceService _fetchAndUpdate = fetchAndUpdate;

    [HttpGet("{ticker}")]
    public async Task<ActionResult<ServiceResponse<PortfolioStock>>> GetStock(string ticker, int userId)
    {
        var result = await _portfolioService.GetStock(ticker, userId);
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<PortfolioStock>>>> GetPortfolioStocks(int userId)
    {
        var result = await _portfolioService.GetPortfolioStocks(userId);
        return Ok(result);
    }

    [HttpGet("update")]
    public async Task<ActionResult<ServiceResponse<List<PortfolioStock>>>> UpdatePriceAndPositionSize(int userId)
    {
        var result = await _fetchAndUpdate.UpdatePriceAndPositionSize(userId);
        return Ok(result);
    }


    [HttpPost("add")]
    public async Task<ActionResult<ServiceResponse<PortfolioStock>>> AddStock([FromBody] PortfolioStock portfolioStock, int userId)
    {
        var result = await _portfolioService.AddStock(portfolioStock, userId);
        return Ok(result);
    }

    [HttpDelete("delete/{stockToDelete}")]
    public async Task<ActionResult<ServiceResponse<bool>>> DeleteStock(string stockToDelete, int userId)
    {
        var result = await _portfolioService.DeleteStock(stockToDelete, userId);
        return Ok(result);
    }


    [HttpPut]
    public async Task<ActionResult<ServiceResponse<PortfolioStock>>> UpdateStock(PortfolioStock portfolioStock, int userId)
    {
        var result = await _portfolioService.UpdateStock(portfolioStock, userId);
        return Ok(result);
    }

}
