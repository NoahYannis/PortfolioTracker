using PortfolioTrackerShared.Models;
using System.Net.Http.Json;
namespace PortfolioTrackerClient.Services.PortfolioService
{
	public class PortfolioService : IPortfolioService
    {

        private string serverBaseDomain = "https://localhost:7207";

        private readonly HttpClient _httpClient;
        private readonly IGetStockInfoService _stockInfoService;

        public PortfolioService(HttpClient httpClient, IGetStockInfoService stockInfoService)
        {
            _httpClient = httpClient;
            _stockInfoService = stockInfoService;
        }


        #region Stock-CRUD

        public List<PortfolioStock> PortfolioStocks { get; set; }

        public event EventHandler<PortfolioChangedArgs>? PortfolioChanged;

        public async Task InitializeAsync()
        {
            PortfolioStocks = await GetDatabaseStocks();
        }

        public void OnPortfolioChanged(List<PortfolioStock> portfolioStocks, PortfolioStock? modifiedStock = null, PortfolioAction portfolioAction = 0)
        {
            PortfolioChanged?.Invoke(this, new PortfolioChangedArgs(portfolioStocks, modifiedStock, portfolioAction));
        }

        public async Task<PortfolioStock> AddStock(PortfolioStock stock)
        {
            var response = await _httpClient.PostAsJsonAsync($"{serverBaseDomain}/api/portfolio", stock);
            var newStock = (await response.Content.ReadFromJsonAsync<ServiceResponse<PortfolioStock>>()).Data;
            OnPortfolioChanged(PortfolioStocks = await GetDatabaseStocks(), PortfolioStocks.FirstOrDefault(s => s.Ticker == stock.Ticker), PortfolioAction.Added);
            return newStock;
        }

        public async Task<bool> DeleteStock(string stockToDelete)
        {
            var response = await _httpClient.DeleteAsync($"{serverBaseDomain}/api/portfolio/{stockToDelete}");

            if (response.IsSuccessStatusCode)
                OnPortfolioChanged(PortfolioStocks = await GetDatabaseStocks(), PortfolioStocks.FirstOrDefault(s => s.Ticker == stockToDelete), PortfolioAction.Deleted);

            return response.IsSuccessStatusCode;

        }

        public async Task<PortfolioStock> GetDatabaseStock(string ticker)
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<PortfolioStock>>($"{serverBaseDomain}/api/portfolio/{ticker}");
            return response.Data;
        }

        public async Task<List<PortfolioStock>> GetDatabaseStocks()
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


        /// <summary>
        /// Fetches the current stock price for each stock inside the portfolio
        /// </summary>
        /// <returns></returns>
        public async Task<bool> UpdatePriceAndPositionSize()
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<PortfolioStock>>>($"{serverBaseDomain}/api/portfolio/update");

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
