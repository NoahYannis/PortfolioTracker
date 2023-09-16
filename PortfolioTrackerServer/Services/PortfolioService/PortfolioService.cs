using Microsoft.EntityFrameworkCore;
using PortfolioTrackerServer.Data;
using PortfolioTrackerShared.Models;
using PortfolioTrackerShared.Models.UserModels;
using PortfolioTrackerShared.Other;

namespace PortfolioTrackerServer.Services.PortfolioService
{
    public class PortfolioService : IPortfolioService
    {
        private readonly DataContext _dataContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PortfolioService(DataContext dataContext, IHttpContextAccessor httpContextAccessor)
        {
            _dataContext = dataContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public User PortfolioOwner { get; private set; } = new();

        private List<PortfolioStock> _portfolioStocks;
        public List<PortfolioStock> PortfolioStocks
        {
            get
            {
                if (_portfolioStocks == null)
                {
                    InitializePortfolioAsync(PortfolioOwner.UserId).Wait();
                }

                return _portfolioStocks;
            }
            set => _portfolioStocks = value;
        }

        public List<Order> Orders { get; set; } = new();

        #region Stock

        public async Task InitializePortfolioAsync(int userId)
        {
            var response = await GetPortfolioStocks(userId);
            PortfolioStocks = response?.Data ?? new();
        }

        public async Task<ServiceResponse<PortfolioStock>> GetStock(string ticker)
        {
            var stock = await _dataContext.Stocks.FirstOrDefaultAsync(s => s.Ticker == ticker);

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

            Portfolio userPortfolio = await _dataContext.Portfolios.FirstOrDefaultAsync(p => p.UserId == userId);

            if (userPortfolio is null)
            {
                response.Success = false;
                response.Message = $"Portfolio not found for user {userId}.";
                return response;
            }

            response.Data = await _dataContext.Stocks.Where(s => s.PortfolioId == userPortfolio.Id).ToListAsync();

            if (response.Data.Count == 0)
            {
                response.Success = false;
                response.Message = $"Portfolio is empty for user {userId}.";
            }

            return response;
        }



        public async Task<ServiceResponse<PortfolioStock>> AddStock(PortfolioStock stock, int userId)
        {
            var response = new ServiceResponse<PortfolioStock>();
            var portfolio = await _dataContext.Portfolios.FirstOrDefaultAsync(p => p.UserId == userId);

            if (stock is not null && portfolio is not null && !portfolio.Positions.Contains(stock))
            {
                portfolio.Positions.Add(stock);
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

        public async Task<ServiceResponse<PortfolioStock>> UpdateStock(PortfolioStock stock, int userId)
        {
            var portfolio = await _dataContext.Portfolios.FirstOrDefaultAsync(p => p.UserId == userId) ?? new();
            var dbStock = await _dataContext.Stocks.FirstOrDefaultAsync(s => s.Ticker == stock.Ticker && s.PortfolioId == portfolio.Id);

            if (dbStock is null)
                return new ServiceResponse<PortfolioStock> { Data = null, Success = false, Message = "Not found." };

            dbStock.Ticker = stock.Ticker;
            dbStock.BuyInPrice = stock.BuyInPrice;
            dbStock.SharesOwned = stock.SharesOwned;
            dbStock.RelativePerformance = stock.RelativePerformance;
            dbStock.AbsolutePerformance = stock.AbsolutePerformance;
            dbStock.DividendYield = stock.DividendYield;
            dbStock.Industry = stock.Industry;
            dbStock.PositionSize = stock.SharesOwned * (stock.CurrentPrice ?? 10);

            await _dataContext.SaveChangesAsync();

            return new ServiceResponse<PortfolioStock> { Data = dbStock };
        }


        public async Task<ServiceResponse<bool>> DeleteStock(string stockToDelete, int userId)
        {
            var portfolio = await _dataContext.Portfolios.FirstOrDefaultAsync(p => p.UserId == userId) ?? new();
            var dbStock = await _dataContext.Stocks.FirstOrDefaultAsync(s => s.Ticker == stockToDelete && s.PortfolioId == portfolio.Id);

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

            return new ServiceResponse<bool> { Data = true };
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