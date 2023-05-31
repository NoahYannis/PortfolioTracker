namespace PortfolioTracker.Models
{
    public enum OrderType
    {
        Buy,
        Sell
    }

    public class Order
    {
        public int OrderNumber { get; set; }
        public OrderType OrderType { get; set; }
        public DateTime Date { get; set; }
        public string Ticker { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
