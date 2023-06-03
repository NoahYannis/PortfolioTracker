using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PortfolioTracker
{

    public class Stock
    {
        [Required, StringLength(5, MinimumLength = 1, ErrorMessage = "Ticker Must Be Between 1 And 5 Characters")]
        public string Ticker { get; set; } = string.Empty;

        [Required, Range(0.01f, double.MaxValue, ErrorMessage = "Position Size Must Be Greater Than $0.")]
        public decimal? PositionSize { get; set; } = null;

        [Required, Range(0.01, double.MaxValue, ErrorMessage = "Shares Owned Must Be Greater Than Zero.")]
        public decimal? SharesOwned { get; set; } = null;

        [Required, Range(0.1, double.MaxValue, ErrorMessage = "Buy In Price Must Be Greater Than $0.")]
        public decimal? BuyInPrice { get; set; } = null;

        [Range(0, 200)]
        public decimal? DividendYield { get; set; } = null;

        public decimal? RelativePerformance
        {
            get
            {
                return ((Close - BuyInPrice) / BuyInPrice) * 100;
            }
            set
            {
                RelativePerformance = value;
            }
        }

        public decimal? AbsolutePerformance
        {
            get
            {
                return (Close - BuyInPrice) * SharesOwned;
            }
            set
            {
                AbsolutePerformance = value;
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
