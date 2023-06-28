using Microsoft.EntityFrameworkCore;
using PortfolioTrackerServer.Data;
using PortfolioTrackerShared.Models;
using PortfolioTrackerShared.Other;

namespace PortfolioTrackerServer.Services.PortfolioService
{
    public class PortfolioService : IPortfolioService
    {
        private readonly DataContext _dataContext;
        private readonly HttpContextAccessor _httpContextAccessor;

        public PortfolioService(DataContext dataContext, HttpContextAccessor httpContextAccessor)
        {
            _dataContext = dataContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public List<PortfolioStock> PortfolioStocks { get; set; } = new();
        public List<Order> Orders { get; set; } = new();

        #region Stock
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


        public async Task<ServiceResponse<List<PortfolioStock>>> GetStocks()
        {
            return new ServiceResponse<List<PortfolioStock>> { Data = await _dataContext.Stocks.ToListAsync() };
        }


        public async Task<ServiceResponse<PortfolioStock>> AddStock(PortfolioStock stock)
        {
            if (stock is not null && _dataContext.Stocks.Contains(stock) is false)
            {
                _dataContext.Stocks.Add(stock);
                await _dataContext.SaveChangesAsync();
            }

            return new ServiceResponse<PortfolioStock> { Data = stock };
        }

        public async Task<ServiceResponse<bool>> UpdateStock(PortfolioStock stock)
        {
            var dbStock = await _dataContext.Stocks.FirstOrDefaultAsync(s => s.Ticker == stock.Ticker);

            if (dbStock is not null)
            {
                dbStock.Ticker = stock.Ticker;
                dbStock.BuyInPrice = stock.BuyInPrice;
                dbStock.SharesOwned = stock.SharesOwned;
                dbStock.RelativePerformance = stock.RelativePerformance;
                dbStock.AbsolutePerformance = stock.AbsolutePerformance;
                dbStock.DividendYield = stock.DividendYield;
                dbStock.Industry = stock.Industry;
                dbStock.PositionSize = stock.PositionSize;

                await _dataContext.SaveChangesAsync();
            }
            else
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Message = "Stock does not exist.",
                    Success = false
                };
            }

            return new ServiceResponse<bool> { Data = true };
        }

        public async Task<ServiceResponse<bool>> DeleteStock(string ticker)
        {
            var dbStock = await _dataContext.Stocks.FirstOrDefaultAsync(s => s.Ticker == ticker);

            if (dbStock is null)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Message = $"There was no stock '{ticker}' to delete in the database.",
                    Success = false
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