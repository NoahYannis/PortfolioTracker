using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PortfolioTracker
{

    public class Stock
    {
        [Required, StringLength(5, MinimumLength = 1, ErrorMessage = "Value must be between 1 and 5 characters")]
        public string Ticker { get; set; } = string.Empty;

        [Required, MinLength(0, ErrorMessage = "Value must be greater than zero")]
        public decimal PositionSize = decimal.Zero;

        [Required, MinLength(0, ErrorMessage = "Value must be greater than zero")]
        public decimal SharesOwned = decimal.Zero;

        [Required, Range(0.1f, double.MaxValue, ErrorMessage = "Value must be greater than zero")]
        public decimal BuyInPrice = decimal.Zero;

        [Range(0, 200)]
        public decimal DividendYield = decimal.Zero;

        public decimal RelativePerformance
        {
            get
            {
                return ((Close - BuyInPrice) / BuyInPrice) * 100;
            }
            set
            {
                BuyInPrice = value;
            }
        }

        public decimal AbsolutePerformance
        {
            get
            {
                return (Close - BuyInPrice) * SharesOwned;
            }
            set
            {
                SharesOwned = value;
            }
        }

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
