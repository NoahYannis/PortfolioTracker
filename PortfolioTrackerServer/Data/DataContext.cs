using Microsoft.EntityFrameworkCore;
using PortfolioTrackerShared.Models;
using PortfolioTrackerShared.Models.UserModels;

namespace PortfolioTrackerServer.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }


        public DbSet<User> Users { get; set; }
        public DbSet<UserSettings> UserSettings { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<PortfolioStock> Stocks { get; set; }
        public DbSet<Order> Orders { get; set; }


    }
}
