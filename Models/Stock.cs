namespace PortfolioTracker.Models
{
    public class Stock
    {
        public string Name { get; set; } = string.Empty;
        public string TickerSymbol { get; set; } = string.Empty;
        public string Sector { get; set; } = string.Empty;
        public string FromCountry { get; set; } = string.Empty;
        public string _52_Week_Range {get;set;} = string.Empty;


        public decimal Price { get; set; }
        public decimal PE_Ratio { get; set; }
        public decimal EPS_PerShare { get; set; }
        public decimal MarketCap { get; set; }
        public decimal _52_Week_Low { get; set; }
        public decimal _52_Week_High { get; set; }


        public bool PaysDividend { get; set; }

    }
}
