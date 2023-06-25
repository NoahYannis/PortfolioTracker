using PortfolioTrackerShared.Other;
using PortfolioTrackerShared.Models;

namespace PortfolioTrackerClient.Services.PortfolioService
{
    public class PortfolioService : IPortfolioService
    {
        #region Stock-CRUD

        // Simulating the portfolio for now
        public List<PortfolioStock> PortfolioStocks { get; set; } = new List<PortfolioStock>()
        {
          new PortfolioStock { Ticker = "GOOGL", PositionSize = 28, SharesOwned = 1, BuyInPrice = 10},
          new PortfolioStock { Ticker = "MSFT", PositionSize = 18, SharesOwned = 1, BuyInPrice = 10},
          new PortfolioStock { Ticker = "ABBV", PositionSize = 11, SharesOwned = 1, BuyInPrice = 10},
          new PortfolioStock { Ticker = "O", PositionSize = 5, SharesOwned = 1, BuyInPrice = 10},
        };

        public event EventHandler<PortfolioChangedArgs>? PortfolioChanged;

        public void OnPortfolioChanged(List<PortfolioStock> portfolioStocks, PortfolioStock? modifiedStock = null, PortfolioAction portfolioAction = 0)
        {
            PortfolioChanged?.Invoke(this, new PortfolioChangedArgs(portfolioStocks, modifiedStock, portfolioAction));
        }

        public async Task<bool> AddStock(PortfolioStock stock)
        {
            // Avoid duplicate tickers.
            if (!PortfolioStocks.Any(s => s.Ticker == stock.Ticker))
            {
                PortfolioStocks.Add(stock);
                OnPortfolioChanged(PortfolioStocks, stock, PortfolioAction.Added);

                return true;
            }

            return false;
        }

        public async Task<bool> DeleteStock(string ticker)
        {
            PortfolioStock stockToRemove = PortfolioStocks.FirstOrDefault(s => s.Ticker == ticker);

            if (stockToRemove != null)
            {
                PortfolioStocks.Remove(stockToRemove);
                OnPortfolioChanged(PortfolioStocks, stockToRemove, PortfolioAction.Deleted);
                return true;
            }

            return false;
        }

        public async Task<PortfolioStock> GetStock(string ticker)
        {
            PortfolioStock stock = PortfolioStocks.FirstOrDefault(s => s.Ticker == ticker);
            return await Task.FromResult(stock ?? new PortfolioStock());
        }

        public async Task<List<PortfolioStock>> GetStocks()
        {
            return await Task.FromResult(PortfolioStocks);
        }

        /// <summary>
        /// Update the specified stock
        /// </summary>
        /// <param name="stock"></param>
        /// <returns>Update success</returns>
        public async Task<bool> UpdateStock(PortfolioStock stock)
        {
            PortfolioStock stockToUpdate = PortfolioStocks.FirstOrDefault(s => s.Ticker == stock.Ticker);


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
                    OnPortfolioChanged(PortfolioStocks, stockToUpdate, PortfolioAction.Modified);

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
            return order is not null ? await Task.FromResult(order) : new Order();
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
