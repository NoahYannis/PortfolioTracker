using System.Text.Json.Serialization;

namespace PortfolioTracker
{
    public enum Industry
    {
        Technology,
        Healthcare,
        Financial,
        Energy,
        Materials,
        Real_Estate,
        Consumer_Staples,
        Consumer_Discretionary,
        Utilities,
        Industrials,
        Other,
        NotDefinedByUser,
    }

    public class Stock
    {
        public string Ticker { get; set; } = String.Empty;

        public decimal PositionSize = decimal.Zero;

        public decimal SharesOwned = decimal.Zero;

        public decimal BuyInPrice = decimal.Zero;

        public decimal ProcentualPerformance => ((Close - BuyInPrice) / BuyInPrice) * 100;

        public decimal AbsolutePerformance => (Close - BuyInPrice) * SharesOwned;

        public decimal DividendYield = decimal.Zero;

        public Industry Industry { get; set; }

        #region Polygon API Properties

        [JsonPropertyName("afterHours")]
        public decimal AfterHours { get; set; }

        [JsonPropertyName("afterHours")]
        public decimal Open { get; set; }

        [JsonPropertyName("close")]
        public decimal Close { get; set; }

        [JsonPropertyName("preMarket")]
        public decimal PreMarket { get; set; }

        [JsonPropertyName("high")]
        public decimal High { get; set; }

        [JsonPropertyName("low")]
        public decimal Low { get; set; }
        #endregion
    }
}
