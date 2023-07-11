using Microsoft.AspNetCore.Mvc;
using PortfolioTrackerServer.Services.GetStockInfoService;
using PortfolioTrackerServer.Services.PortfolioService;
using PortfolioTrackerShared.Models;
using PortfolioTrackerShared.Other;
using System.Net;

namespace PortfolioTrackerServer.Controller
{
    [Route("api/polygon")]
    [ApiController]
    public class PolygonQueryController : ControllerBase
    {
        private readonly IGetStockInfoService _stockInfoService;

        public PolygonQueryController(IGetStockInfoService stockInfoService)
        {
            _stockInfoService = stockInfoService;
        }

        [HttpGet("{tickerSymbol}")]
        public async Task<ActionResult<ServiceResponse<ApiQueryStock>>> GetStockData(string tickerSymbol)
        {
            var result = await _stockInfoService.GetStockData(tickerSymbol);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<ApiQueryStock>>>> GetAllStockData(List<PortfolioStock> portfolioStocks)
        {
            var result = await _stockInfoService.GetAllStockData(portfolioStocks);
            return Ok(result);
        }

    }
}
