using Microsoft.EntityFrameworkCore;
using PortfolioTrackerShared.Models;

namespace PortfolioTrackerServer.Data
{
    public class DataContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Stock> Stock { get; set; }

    }
}
