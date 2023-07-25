using Microsoft.EntityFrameworkCore;
using PortfolioTrackerShared.Models;

namespace PortfolioTrackerServer.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }


        public DbSet<User> Users { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<PortfolioStock> Stocks { get; set; }
        public DbSet<Order> Orders { get; set; }


    }
}
