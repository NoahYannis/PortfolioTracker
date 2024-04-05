using PortfolioTrackerShared.Models;
using PortfolioTrackerShared.Other;

namespace PortfolioTrackerServer.Services.PortfolioService;

/// <summary>
/// Portfolio functionalities
/// </summary>
public interface IPortfolioService
{
    Task<ServiceResponse<List<PortfolioStock>>> GetPortfolioStocks(int userId);
    Task<ServiceResponse<PortfolioStock>> GetStock(string ticker, int userId);
    Task<ServiceResponse<bool>> DeleteStock(string stockToDelete, int userId);
    Task<ServiceResponse<PortfolioStock>> AddStock(PortfolioStock stock, int userId);
    Task<ServiceResponse<PortfolioStock>> UpdateStock(PortfolioStock stock, int userId);
}
