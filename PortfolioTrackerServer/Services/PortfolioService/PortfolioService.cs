using Microsoft.EntityFrameworkCore;
using PortfolioTrackerServer.Data;
using PortfolioTrackerServer.Services.GetStockInfoService;
using PortfolioTrackerShared.Models;
using PortfolioTrackerShared.Models.UserModels;
using PortfolioTrackerShared.Other;

namespace PortfolioTrackerServer.Services.PortfolioService
{
    public class PortfolioService : IPortfolioService
    {
        private readonly DataContext _dataContext;

        public PortfolioService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        private User PortfolioOwner { get; set; } = new();

        public List<Order> Orders { get; set; } = new();

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
            PortfolioOwner = await _dataContext.Users.SingleOrDefaultAsync(po => po.UserId == userId);

            if (PortfolioOwner is not null)
            {
                PortfolioOwner.Portfolio = await _dataContext.Portfolios.SingleOrDefaultAsync(po => po.UserId == PortfolioOwner.UserId);
                PortfolioOwner.Portfolio.Positions = await _dataContext.Stocks.Where(s => s.PortfolioId == PortfolioOwner.Portfolio.Id).ToListAsync();
            }

            return PortfolioOwner ?? new();
        }

        /// <summary>
        /// Avoid duplicate stocks inside portfolios.
        /// </summary>
        /// <param name="portfolioStock"></param>
        /// <param name="userId"></param>
        /// <returns>Whether a portfolio contains that particular stock</returns>
        public bool ContainsStock(PortfolioStock portfolioStock)
        {
            // Check if any stock in Positions has the same Ticker
            return PortfolioOwner.Portfolio.Positions.Any(stock => stock.Ticker == portfolioStock.Ticker);
        }




        public async Task<ServiceResponse<PortfolioStock>> AddStock(PortfolioStock stock, int userId)
        {
            var response = new ServiceResponse<PortfolioStock>();
            PortfolioOwner = await GetUserPortfolio(userId);

            if (stock is not null && PortfolioOwner.Portfolio is not null && !ContainsStock(stock))
            {
                PortfolioOwner.Portfolio.Positions.Add(stock);
                await _dataContext.SaveChangesAsync();
                response.Data = stock;
            }
            else
            {
                response.Success = false;
                response.Message = $"Failed to add {stock?.Ticker} to the portfolio (stock null or duplicate).";
            }

            return response;
        }

        public async Task<ServiceResponse<PortfolioStock>> UpdateStock(PortfolioStock updatedStock, int userId)
        {
            PortfolioOwner = await GetUserPortfolio(userId);
            var stock = PortfolioOwner.Portfolio.Positions.FirstOrDefault(s => s.Ticker == updatedStock.Ticker) ?? new();

            if (!ContainsStock(stock))
                return new ServiceResponse<PortfolioStock> { Data = null, Success = false, Message = "Not found." };

            stock.BuyInPrice = updatedStock.BuyInPrice;
            stock.SharesOwned = updatedStock.SharesOwned;
            stock.DividendYield = updatedStock.DividendYield;
            stock.Industry = updatedStock.Industry;
            stock.PositionSize = updatedStock.SharesOwned * (updatedStock.CurrentPrice ?? 0);
            stock.AbsolutePerformance = updatedStock.AbsolutePerformance;
            stock.RelativePerformance = updatedStock.RelativePerformance;

            await _dataContext.SaveChangesAsync();

            return new ServiceResponse<PortfolioStock> { Data = stock };
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


        #region Order
        public async Task<ServiceResponse<bool>> DeleteOrder(int orderNumber)
        {
            var dbOrder = await _dataContext.Orders.FirstOrDefaultAsync(o => o.OrderNumber == orderNumber);

            if (dbOrder is null)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Message = $"There was no order '{orderNumber}' to delete in the database.",
                    Success = false
                };
            }

            _dataContext.Orders.Remove(dbOrder);
            await _dataContext.SaveChangesAsync();

            return new ServiceResponse<bool> { Data = true };
        }

        public async Task<ServiceResponse<bool>> CreateOrder(Order order)
        {
            if (order is not null)
            {
                _dataContext.Orders.Add(order);
                await _dataContext.SaveChangesAsync();
            }

            return new ServiceResponse<bool> { Data = true };
        }


        public async Task<ServiceResponse<Order>> GetOrder(int orderNumber)
        {
            var dbOrder = _dataContext.Orders.FirstOrDefaultAsync(o => o.OrderNumber == orderNumber);

            if (dbOrder is null)
            {
                return new ServiceResponse<Order>
                {
                    Data = new Order(),
                    Message = $"There was no order '{orderNumber}' in the database.",
                    Success = false
                };
            }

            return new ServiceResponse<Order> { Data = await dbOrder };
        }

        public async Task<ServiceResponse<List<Order>>> GetOrders()
        {
            var orderNumbers = Orders.Select(o => o.OrderNumber).ToList();
            var dbOrders = await _dataContext.Orders.Where(o => orderNumbers.Contains(o.OrderNumber)).ToListAsync();

            var result = new ServiceResponse<List<Order>> { Data = dbOrders };

            return result;
        }




        public async Task<ServiceResponse<bool>> UpdateOrder(Order order)
        {
            var dbOrder = await _dataContext.Orders.FirstOrDefaultAsync(o => order.OrderNumber == order.OrderNumber);

            if (dbOrder is null)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Message = $"There was no order '{order.OrderNumber}' in the database.",
                    Success = false
                };
            }
            else
            {
                dbOrder.OrderNumber = order.OrderNumber;
                dbOrder.Ticker = order.Ticker;
                dbOrder.Price = order.Price;
                dbOrder.UserId = order.UserId;
                dbOrder.Date = order.Date;
                dbOrder.OrderType = order.OrderType;
                dbOrder.Quantity = order.Quantity;

                await _dataContext.SaveChangesAsync();
            }

            return new ServiceResponse<bool> { Data = true };
        }



        #endregion


    }
}