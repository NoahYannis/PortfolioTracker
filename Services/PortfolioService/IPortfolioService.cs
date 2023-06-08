﻿using PortfolioTracker.Models;
using PortfolioTracker.Other;

namespace PortfolioTracker.Services.PortfolioService
{
    /// <summary>
    /// Portfolio functionalities
    /// </summary>
    public interface IPortfolioService
    {
        #region Stocks 

        List<Stock> PortfolioStocks { get; set; }
        Task<List<Stock>> GetStocks();
        Task<Stock> GetStock(string ticker);
        Task AddStock(Stock stock);
        Task DeleteStock(string ticker);
        Task UpdateStock(Stock stock);

        event EventHandler<PortfolioChangedArgs> PortfolioChanged;
        void OnPortfolioChanged(List<Stock> portfolioStocks);


        #endregion

        #region Orders

        List<Order> Orders { get; set; }
        Task<List<Order>> GetOrders();
        Task<Order> GetOrder(int orderNumber);
        Task UpdateOrder(Order order);
        Task DeleteOrder(int orderNumber);

        
        #endregion


    }
}
