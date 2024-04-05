using Microsoft.EntityFrameworkCore;
using PortfolioTrackerServer.Data;
using PortfolioTrackerShared.Models;
using PortfolioTrackerShared.Models.UserModels;
using PortfolioTrackerShared.Other;

namespace PortfolioTrackerServer.Services.PortfolioService;

public class PortfolioService(DataContext dataContext) : IPortfolioService
{
    private readonly DataContext _dataContext = dataContext;

    private User PortfolioOwner { get; set; } = new();

    #region Stock

    public async Task<ServiceResponse<PortfolioStock>> GetStock(string ticker, int userId)
    {
        PortfolioOwner = await GetUserPortfolio(userId);

        var stock = PortfolioOwner.Portfolio.Positions.FirstOrDefault(s => s.Ticker == ticker) ?? new();

        var result = new ServiceResponse<PortfolioStock>()
        {
            Data = stock ?? new PortfolioStock(),
            Success = stock != null
        };

        return result;
    }


    public async Task<ServiceResponse<List<PortfolioStock>>> GetPortfolioStocks(int userId)
    {
        var response = new ServiceResponse<List<PortfolioStock>>();
        PortfolioOwner = await GetUserPortfolio(userId);

        if (PortfolioOwner.UserId is 0)
        {
            response.Success = false;
            response.Message = $"Portfolio not found for user {userId}.";
            return response;
        }

        response.Data = PortfolioOwner.Portfolio.Positions;

        if (response.Data.Count == 0)
        {
            response.Success = false;
            response.Message = $"Portfolio is empty for user {userId}.";
        }

        return response;
    }


    /// <summary>
    /// Initializes the portfolio user and portfolio.
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    private async Task<User> GetUserPortfolio(int userId)
    {
        PortfolioOwner = await _dataContext.Users.SingleOrDefaultAsync(po => po.UserId == userId) ?? new();

        if (PortfolioOwner.UserId == 0)
        {
            return PortfolioOwner;
        }

        try
        {
            PortfolioOwner.Portfolio = await _dataContext.Portfolios.SingleOrDefaultAsync
                (po => po.UserId == PortfolioOwner.UserId) ?? new();

            PortfolioOwner.Portfolio.Positions = await _dataContext.Stocks.Where
                (s => s.PortfolioId == PortfolioOwner.Portfolio.Id).ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return PortfolioOwner;
    }

    /// <summary>
    /// Avoid duplicate stocks inside portfolios.
    /// </summary>
    /// <param name="portfolioStock"></param>
    /// <param name="userId"></param>
    /// <returns>Whether a portfolio contains that particular stock</returns>
    public bool ContainsStock(PortfolioStock portfolioStock)
    {
        return PortfolioOwner.Portfolio.Positions.Any(stock => stock.Ticker.ToUpper() == portfolioStock.Ticker.ToUpper());
    }


    public async Task<ServiceResponse<PortfolioStock>> AddStock(PortfolioStock stock, int userId)
    {
        var response = new ServiceResponse<PortfolioStock>();
        PortfolioOwner = await GetUserPortfolio(userId);

        if (stock is null || PortfolioOwner.Portfolio is null || ContainsStock(stock))
        {
            response.Success = false;
            response.Message = $"Failed to add {stock?.Ticker} to the portfolio (stock null or duplicate).";
            return response;
        }

        try
        {
            PortfolioOwner.Portfolio.Positions.Add(stock);
            await _dataContext.SaveChangesAsync();
            response.Data = stock;
        }
        catch (Exception e)
        {
            response.Success = false;
            response.Message = $"Failed to save {stock.Ticker} to the database: {e.Message}";
        }

        return response;
    }

    public async Task<ServiceResponse<PortfolioStock>> UpdateStock(PortfolioStock updatedStock, int userId)
    {
        var response = new ServiceResponse<PortfolioStock>();

        PortfolioOwner = await GetUserPortfolio(userId);
        var stock = PortfolioOwner.Portfolio.Positions.FirstOrDefault(s => s.Ticker == updatedStock.Ticker) ?? new();

        if (!ContainsStock(stock))
        {
            response.Success = false;
            response.Message = "Not found";
            return response;
        }

        stock.BuyInPrice = updatedStock.BuyInPrice;
        stock.SharesOwned = updatedStock.SharesOwned;
        stock.DividendYield = updatedStock.DividendYield;
        stock.Industry = updatedStock.Industry;
        stock.PositionSize = updatedStock.PositionSize;
        stock.AbsolutePerformance = updatedStock.AbsolutePerformance;
        stock.RelativePerformance = updatedStock.RelativePerformance;

        try
        {
            await _dataContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            response.Success = false;
            response.Message = $"Failed to update {stock.Ticker}: {e.Message}";
        }

        return response;
    }


    public async Task<ServiceResponse<bool>> DeleteStock(string stockToDelete, int userId)
    {
        PortfolioOwner = await GetUserPortfolio(userId);
        var dbStock = await _dataContext.Stocks.SingleOrDefaultAsync(s => s.Ticker == stockToDelete && s.PortfolioId == PortfolioOwner.Portfolio.Id);

        if (dbStock is null)
        {
            return new ServiceResponse<bool>
            {
                Data = false,
                Success = false,
                Message = $"Stock '{stockToDelete}' not found in Portfolio with user ID {userId}"
            };
        }

        _dataContext.Stocks.Remove(dbStock);
        await _dataContext.SaveChangesAsync();

        return new ServiceResponse<bool> { Data = true, Message = "Stock was deleted." };
    }


    #endregion
}