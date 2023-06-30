using Microsoft.AspNetCore.Mvc;
using PortfolioTrackerServer.Services.GetStockInfoService;
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

    }
}
