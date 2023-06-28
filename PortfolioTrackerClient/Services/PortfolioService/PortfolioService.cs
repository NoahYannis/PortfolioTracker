using PortfolioTrackerShared.Other;
using PortfolioTrackerShared.Models;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;

namespace PortfolioTrackerClient.Services.PortfolioService
{
    public class PortfolioService : IPortfolioService
    {

        private readonly HttpClient _httpClient;

        public PortfolioService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


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

        public async Task<PortfolioStock> AddStock(PortfolioStock stock)
        {
            var result = await _httpClient.PostAsJsonAsync("api/Portfolio", stock);
            var newStock = (await result.Content.ReadFromJsonAsync<ServiceResponse<PortfolioStock>>()).Data;
            OnPortfolioChanged(PortfolioStocks, newStock, PortfolioAction.Added);
            return newStock;
        }

        public async Task DeleteStock(string ticker)
        {
            var response = await _httpClient.DeleteAsync($"api/portfolio/{ticker}");
            OnPortfolioChanged(PortfolioStocks, PortfolioStocks.FirstOrDefault(s => s.Ticker == ticker), PortfolioAction.Deleted);
        }

        public async Task<PortfolioStock> GetStock(string ticker)
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<PortfolioStock>>("api/portfolio");
            return response.Data;
        }

        public async Task<List<PortfolioStock>> GetStocks()
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<PortfolioStock>>>("api/Portfolio");
            return response.Data;
        }

        /// <summary>
        /// Update the specified stock
        /// </summary>
        /// <param name="stock"></param>
        /// <returns>Update success</returns>
        public async Task<PortfolioStock> UpdateStock(PortfolioStock stock)
        {
            var response = await _httpClient.PutAsJsonAsync("api/portfolio", stock);
            OnPortfolioChanged(PortfolioStocks, stock, PortfolioAction.Modified);
            return (await response.Content.ReadFromJsonAsync<ServiceResponse<PortfolioStock>>()).Data;
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
