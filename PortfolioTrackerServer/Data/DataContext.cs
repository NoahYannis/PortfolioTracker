using Microsoft.EntityFrameworkCore;
using PortfolioTrackerShared.Models;
using PortfolioTrackerShared.Models.UserModels;

namespace PortfolioTrackerServer.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserSettings> UserSettings { get; set; }
    public DbSet<Portfolio> Portfolios { get; set; }
    public DbSet<PortfolioStock> Stocks { get; set; }
}
