namespace PortfolioTracker.Services.PortfolioService
{
    public class PortfolioService : IPortfolioService
    {
        public List<Stock> PortfolioStocks { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Task AddStock(Stock stock)
        {
            throw new NotImplementedException();
        }

        public Task DeleteStock(string ticker)
        {
            throw new NotImplementedException();
        }

        public Task GetStock(string ticker)
        {
            throw new NotImplementedException();
        }

        public Task GetStocks()
        {
            throw new NotImplementedException();
        }

        public Task UpdateStock(Stock stock)
        {
            throw new NotImplementedException();
        }
    }
}
