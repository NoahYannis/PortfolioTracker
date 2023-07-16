using Microsoft.EntityFrameworkCore;
using PortfolioTrackerServer.Data;
using PortfolioTrackerServer.Services.GetStockInfoService;
using PortfolioTrackerShared.Models;
using PortfolioTrackerShared.Other;
using System.Diagnostics;

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

        private List<PortfolioStock> _portfolioStocks;
        public List<PortfolioStock> PortfolioStocks
        {
            get
            {
                if (_portfolioStocks == null)
                {
                    InitializeAsync().Wait();
                }
                return _portfolioStocks;
            }
            set => _portfolioStocks = value;
        }

        public List<Order> Orders { get; set; } = new();

        #region Stock

        public async Task InitializeAsync()
        {
            var response = await GetDatabaseStocks();
            PortfolioStocks = response?.Data;
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


        public async Task<ServiceResponse<List<PortfolioStock>>> GetDatabaseStocks()
        {
            return new ServiceResponse<List<PortfolioStock>> { Data = PortfolioStocks = await _dataContext.Stocks.ToListAsync() };
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

        public async Task<ServiceResponse<PortfolioStock>> UpdateStock(PortfolioStock stock)
        {
            var dbStock = await _dataContext.Stocks.FirstOrDefaultAsync(s => s.Ticker == stock.Ticker);

            if (dbStock is null)
                return new ServiceResponse<PortfolioStock> { Data = null, Success = false, };

            dbStock.Ticker = stock.Ticker;
            dbStock.BuyInPrice = stock.BuyInPrice;
            dbStock.SharesOwned = stock.SharesOwned;
            dbStock.RelativePerformance = stock.RelativePerformance;
            dbStock.AbsolutePerformance = stock.AbsolutePerformance;
            dbStock.DividendYield = stock.DividendYield;
            dbStock.Industry = stock.Industry;
            dbStock.PositionSize = stock.SharesOwned * stock.CurrentPrice;

            await _dataContext.SaveChangesAsync();

            return new ServiceResponse<PortfolioStock> { Data = dbStock };
        }


        public async Task<ServiceResponse<bool>> DeleteStock(string stockToDelete)
        {
            var dbStock = await _dataContext.Stocks.FirstOrDefaultAsync(s => s.Ticker == stockToDelete);

            if (dbStock is null)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
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