﻿using PortfolioTrackerShared.Models;
using PortfolioTrackerShared.Other;

namespace PortfolioTrackerServer.Services.PortfolioService
{
    /// <summary>
    /// Portfolio functionalities
    /// </summary>
    public interface IPortfolioService
    {
        #region Stocks 

        List<PortfolioStock> PortfolioStocks { get; set; }
        Task<ServiceResponse<List<PortfolioStock>>> GetStocks();
        Task<ServiceResponse<PortfolioStock>> GetStock(string ticker);
        Task<ServiceResponse<bool>> DeleteStock(string ticker);
        Task<ServiceResponse<bool>> AddStock(PortfolioStock stock);
        Task<ServiceResponse<bool>> UpdateStock(PortfolioStock stock);

        //event EventHandler<PortfolioChangedArgs> PortfolioChanged;
        //void OnPortfolioChanged(List<PortfolioStock> portfolioStocks, PortfolioStock? deletedStock = null);


        #endregion

        #region Orders

        List<Order> Orders { get; set; }
        Task<ServiceResponse<List<Order>>> GetOrders();
        Task<ServiceResponse<Order>> GetOrder(int orderNumber);
        Task<ServiceResponse<bool>> CreateOrder(Order order);
        Task<ServiceResponse<bool>> UpdateOrder(Order order);
        Task<ServiceResponse<bool>> DeleteOrder(int orderNumber);


        #endregion


    }
}