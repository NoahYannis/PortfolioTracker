using PortfolioTracker.Models;
using PortfolioTracker.Other;

namespace PortfolioTracker.Services.PortfolioService
{
    public class PortfolioService : IPortfolioService
    {

        #region Stock-CRUD
        public List<Stock> PortfolioStocks { get; set; } = new List<Stock>();

        public event EventHandler<PortfolioChangedArgs> PortfolioChanged;

        public void OnPortfolioChanged(List<Stock> portfolioStocks)
        {
            PortfolioChanged?.Invoke(this, new PortfolioChangedArgs(portfolioStocks));
        }

        public Task AddStock(Stock stock)
        {
            PortfolioStocks.Add(stock);
            OnPortfolioChanged(PortfolioStocks);
            return Task.CompletedTask;
        }

        public Task DeleteStock(string ticker)
        {
            Stock stockToRemove = PortfolioStocks.FirstOrDefault(s => s.Ticker == ticker);

            if (stockToRemove != null)
            {
                PortfolioStocks.Remove(stockToRemove);
                OnPortfolioChanged(PortfolioStocks);
            }

            return Task.CompletedTask;
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

        public Task UpdateStock(Stock stock)
        {
            Stock stockToUpdate = PortfolioStocks.FirstOrDefault(s => s.Ticker == stock.Ticker);

            if (stockToUpdate != null)
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
            }

            return Task.CompletedTask;
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
