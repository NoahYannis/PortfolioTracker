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

        public User PortfolioOwner { get; set; } = new();

        public List<PortfolioStock> PortfolioStocks { get; set; }

        public event EventHandler<PortfolioChangedArgs>? PortfolioChanged;

        public async Task InitializePortfolioAsync(int userId)
        {
            PortfolioStocks = await GetPortfolioStocks(userId);
        }

        public void OnPortfolioChanged(List<PortfolioStock> portfolioStocks, PortfolioStock? modifiedStock = null, PortfolioAction portfolioAction = 0)
        {
            PortfolioChanged?.Invoke(this, new PortfolioChangedArgs(portfolioStocks, modifiedStock));
        }

        public async Task<PortfolioStock> AddStock(PortfolioStock stock, int userId)
        {
            var response = await _httpClient.PostAsJsonAsync($"{serverBaseDomain}/api/portfolio/add?userId={userId}", stock);
            var newStock = (await response.Content.ReadFromJsonAsync<ServiceResponse<PortfolioStock>>()).Data;
            OnPortfolioChanged(PortfolioStocks = await GetPortfolioStocks(userId), PortfolioStocks.FirstOrDefault(s => s.Ticker == stock.Ticker), PortfolioAction.Added);
            return newStock;
        }

        public async Task<bool> DeleteStock(string stockToDelete, int userId)
        {
            var response = await _httpClient.DeleteAsync($"{serverBaseDomain}/api/portfolio/delete/{stockToDelete}?userId={userId}");

            if (response.IsSuccessStatusCode)
                OnPortfolioChanged(PortfolioStocks = await GetPortfolioStocks(userId), null, PortfolioAction.Deleted);

            return response.IsSuccessStatusCode;

        }

        public async Task<PortfolioStock> GetDatabaseStock(string ticker)
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<PortfolioStock>>($"{serverBaseDomain}/api/portfolio/{ticker}");
            return response.Data;
        }

        public async Task<List<PortfolioStock>> GetPortfolioStocks(int userId) 
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<PortfolioStock>>>($"{serverBaseDomain}/api/portfolio?userId={userId}");
            return response.Data;
        }

        /// <summary>
        /// Update the specified stock
        /// </summary>
        /// <param name="stock"></param>
        /// <returns>Update success</returns>
        public async Task<ServiceResponse<PortfolioStock>> UpdateStock(PortfolioStock stock, int userId)
        {
            var response = await _httpClient.PutAsJsonAsync($"{serverBaseDomain}/api/portfolio?userId={userId}", stock);

            Console.Write($"relperf bef: {stock.RelativePerformance}");

            if (response.IsSuccessStatusCode)
                OnPortfolioChanged(PortfolioStocks, stock, PortfolioAction.Modified);

            Console.Write($"relperf aft: {stock.RelativePerformance}");

            return (await response.Content.ReadFromJsonAsync<ServiceResponse<PortfolioStock>>());
        }


        /// <summary>
        /// Fetches the current stock price for each stock inside the portfolio
        /// </summary>
        /// <returns></returns>
        public async Task<bool> UpdatePriceAndPositionSize(int userId)
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<PortfolioStock>>>($"{serverBaseDomain}/api/portfolio/update?userId={userId}");

            if (response.Success)
            {
                for (int i = 0; i < response.Data.Count; i++)
                {
                    OnPortfolioChanged(PortfolioStocks, PortfolioStocks[i], PortfolioAction.Modified);
                }

                return true;
            }

            return false;
        }

        #endregion

        public decimal GetTotalValue()
        {
            return PortfolioStocks.Sum(s => s.PositionSize) ?? 0;
        }

        public decimal GetTotalAbsolutePerformance()
        {
            return PortfolioStocks.Sum(s => s.AbsolutePerformance) ?? 0;
        }

        public decimal GetTotalRelativePerformance()
        {
            return Math.Round(PortfolioStocks.Average(s => s.RelativePerformance ?? 0), 2);
        }

        #region Order-CRUD

        public List<Order> Orders { get; set; } = new List<Order>();

        // Implement OnOrdersChangedEvent;


        /// <summary>
        /// Gets all orders from the data base
        /// </summary>
        /// <returns></returns>
        public async Task<List<Order>> GetOrders()
        {
            return await Task.FromResult(Orders);
        }

        /// <summary>
        /// Gets one order from the data base
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
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
