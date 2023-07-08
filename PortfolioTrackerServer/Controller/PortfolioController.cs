using Microsoft.AspNetCore.Mvc;
using PortfolioTrackerServer.Services.PortfolioService;
using PortfolioTrackerShared.Models;
using PortfolioTrackerShared.Other;
using System.Net.Http;
using System.Net;
using System;

namespace PortfolioTrackerServer.Controller
{
    [ApiController]
    [Route("api/portfolio")]
    public class PortfolioController : ControllerBase
    {

        private readonly IPortfolioService _portfolioService;

        public PortfolioController(IPortfolioService portfolioService)
        {
            _portfolioService = portfolioService;
        }

        [HttpGet("{ticker}")]
        public async Task<ActionResult<ServiceResponse<PortfolioStock>>> GetStock(string ticker)
        {
            var result = await _portfolioService.GetStock(ticker);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<PortfolioStock>>>> GetStocks()
        {
            var result = await _portfolioService.GetStocks();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<PortfolioStock>>> AddStock([FromBody] PortfolioStock portfolioStock)
        {
            var result = await _portfolioService.AddStock(portfolioStock);
            return Ok(result);
        }

        [HttpDelete("{ticker}")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteStock(string ticker)
        {
            var result = await _portfolioService.DeleteStock(ticker);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<PortfolioStock>>> UpdateStock(PortfolioStock portfolioStock)
        {
            var result = await _portfolioService.UpdateStock(portfolioStock);
            return Ok(result);
        }

    }
}
