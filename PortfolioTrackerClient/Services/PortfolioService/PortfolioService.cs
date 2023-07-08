using PortfolioTrackerShared.Other;
using PortfolioTrackerShared.Models;
using System.Net.Http.Json;

namespace PortfolioTrackerClient.Services.PortfolioService
{
    public class PortfolioService : IPortfolioService
    {

        private string serverBaseDomain = "https://localhost:7207";
        private readonly HttpClient _httpClient;

        public PortfolioService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        #region Stock-CRUD

        public List<PortfolioStock> PortfolioStocks { get; set; }

        public event EventHandler<PortfolioChangedArgs>? PortfolioChanged;

        public async Task InitializeAsync()
        {
            PortfolioStocks = await GetStocks();
        }

        public void OnPortfolioChanged(List<PortfolioStock> portfolioStocks, PortfolioStock? modifiedStock = null, PortfolioAction portfolioAction = 0)
        {
            PortfolioChanged?.Invoke(this, new PortfolioChangedArgs(portfolioStocks, modifiedStock, portfolioAction));
        }

        public async Task<PortfolioStock> AddStock(PortfolioStock stock)
        {
            var response = await _httpClient.PostAsJsonAsync($"{serverBaseDomain}/api/portfolio", stock);
            var newStock = (await response.Content.ReadFromJsonAsync<ServiceResponse<PortfolioStock>>()).Data;
            OnPortfolioChanged(PortfolioStocks, newStock, PortfolioAction.Added);
            return newStock;
        }

        public async Task<bool> DeleteStock(string stockToDelete)
        {
            var response = await _httpClient.DeleteAsync($"{serverBaseDomain}/api/portfolio/{stockToDelete}");

            if (response.IsSuccessStatusCode)
                OnPortfolioChanged(PortfolioStocks, PortfolioStocks.FirstOrDefault(s => s.Ticker == stockToDelete) , PortfolioAction.Deleted);

            return response.IsSuccessStatusCode;

        }

        public async Task<PortfolioStock> GetStock(string ticker)
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<PortfolioStock>>($"{serverBaseDomain}/api/portfolio/{ticker}");
            return response.Data;
        }

        public async Task<List<PortfolioStock>> GetStocks()
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<PortfolioStock>>>($"{serverBaseDomain}/api/portfolio");
            return response.Data;
        }

        /// <summary>
        /// Update the specified stock
        /// </summary>
        /// <param name="stock"></param>
        /// <returns>Update success</returns>
        public async Task<ServiceResponse<PortfolioStock>> UpdateStock(PortfolioStock stock)
        {
            var response = await _httpClient.PutAsJsonAsync($"{serverBaseDomain}/api/portfolio", stock);

            if (response.IsSuccessStatusCode)
                OnPortfolioChanged(PortfolioStocks, stock, PortfolioAction.Modified);

            return (await response.Content.ReadFromJsonAsync<ServiceResponse<PortfolioStock>>());
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
