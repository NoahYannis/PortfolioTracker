using PortfolioTracker.Models;
using PortfolioTracker.Other;

namespace PortfolioTracker.Services.PortfolioService
{
    public class PortfolioService : IPortfolioService
    {

        #region Stock-CRUD

        // Simulating the portfolio for now
        public List<Stock> PortfolioStocks { get; set; } = new List<Stock>()
        {
          new Stock { Ticker = "GOOGL", PositionSize = 28, SharesOwned = 1, BuyInPrice = 10},
          new Stock { Ticker = "MSFT", PositionSize = 18, SharesOwned = 1, BuyInPrice = 10},
          new Stock { Ticker = "ABBV", PositionSize = 11, SharesOwned = 1, BuyInPrice = 10},
          new Stock { Ticker = "O", PositionSize = 5, SharesOwned = 1, BuyInPrice = 10},
        };

        public event EventHandler<PortfolioChangedArgs>? PortfolioChanged;

        public void OnPortfolioChanged(List<Stock> portfolioStocks, Stock? deletedStock = null)
        {
            PortfolioChanged?.Invoke(this, new PortfolioChangedArgs(portfolioStocks, deletedStock));
        }

        public async Task<bool> AddStock(Stock stock)
        {
            // Avoid duplicate tickers.
            if (!PortfolioStocks.Any(s => s.Ticker == stock.Ticker))
            {
                PortfolioStocks.Add(stock);
                OnPortfolioChanged(PortfolioStocks);

                return true;
            }

            return false;
        }

        public async Task<bool> DeleteStock(string ticker)
        {
            Stock stockToRemove = PortfolioStocks.FirstOrDefault(s => s.Ticker == ticker);
            Stock stock2 = stockToRemove;

            if (stockToRemove != null)
            {
                PortfolioStocks.Remove(stockToRemove);
                OnPortfolioChanged(PortfolioStocks, stock2);
                return true;
            }

            return false;
        }

        public async Task<Stock> GetStock(string ticker)
        {
            Stock stock = PortfolioStocks.FirstOrDefault(s => s.Ticker == ticker);
            return await Task.FromResult(stock ?? new Stock());
        }

        public async Task<List<Stock>> GetStocks()
        {
            return await Task.FromResult(PortfolioStocks);
        }

        /// <summary>
        /// Update the specified stock
        /// </summary>
        /// <param name="stock"></param>
        /// <returns>Update success</returns>
        public async Task<bool> UpdateStock(Stock stock)
        {
            Stock stockToUpdate = PortfolioStocks.FirstOrDefault(s => s.Ticker == stock.Ticker);


            if (stockToUpdate != null && PortfolioStocks.Count(s => s.Ticker == stockToUpdate.Ticker) == 1)
            {
                    stockToUpdate.Ticker = stock.Ticker;
                    stockToUpdate.BuyInPrice = stock.BuyInPrice;
                    stockToUpdate.SharesOwned = stock.SharesOwned;
                    stockToUpdate.AbsolutePerformance = stock.RelativePerformance;
                    stockToUpdate.AbsolutePerformance = stock.AbsolutePerformance;
                    stockToUpdate.DividendYield = stock.DividendYield;
                    stockToUpdate.Industry = stock.Industry;
                    stockToUpdate.PositionSize = stock.PositionSize;
                    OnPortfolioChanged(PortfolioStocks);

                return true;
            }

            return false;
        }

        #endregion

        #region Order-CRUD

        public List<Order> Orders { get; set; } = new List<Order>();

        // Implement OnOrdersChangedEvent;

        public async Task<List<Order>> GetOrders()
        {
            return await Task.FromResult(Orders);
        }

        public async Task<Order> GetOrder(int orderNumber)
        {
            Order order = Orders.FirstOrDefault(o => o.OrderNumber == orderNumber);

            if (order != null)
            {
                return await Task.FromResult(order);
            }
            else
            {
                return await Task.FromResult(new Order() { Message = $"Couldn't retrieve Order {orderNumber}" });
            }
        }

        public Task UpdateOrder(Order order)
        {
            Order orderToUpdate = Orders.FirstOrDefault(o => o.OrderNumber == order.OrderNumber);

            if (orderToUpdate != null)
            {
                order.Price = orderToUpdate.Price;
                order.Quantity = orderToUpdate.Quantity;
                order.Ticker = orderToUpdate.Ticker;
                order.OrderType = orderToUpdate.OrderType;
                order.Date = orderToUpdate.Date;
                order.Message = orderToUpdate.Message;
            }

            return Task.CompletedTask;
        }

        public Task DeleteOrder(int orderNumber)
        {
            Order orderToDelete = Orders.FirstOrDefault(o => o.OrderNumber == orderNumber);

            if (orderToDelete != null)
            {
                Orders.Remove(orderToDelete);
            }

            return Task.CompletedTask;
        }

        #endregion
    }
}
