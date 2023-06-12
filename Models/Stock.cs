using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PortfolioTracker
{

    public class Stock
    {
        [Required, StringLength(5, MinimumLength = 1, ErrorMessage = "Ticker Must Be Between 1 And 5 Characters")]
        public string Ticker { get; set; } = "Test"; // Debug values to save time during testing

        [Required, Range(0.01f, double.MaxValue, ErrorMessage = "Position Size Must Be Greater Than $0.")]
        public decimal? PositionSize { get; set; } = 100;

        [Required, Range(0.01, double.MaxValue, ErrorMessage = "Shares Owned Must Be Greater Than Zero.")]
        public decimal? SharesOwned { get; set; } = 10;

        [Required, Range(0.1, double.MaxValue, ErrorMessage = "Buy In Price Must Be Greater Than $0.")]
        public decimal? BuyInPrice { get; set; } = 10;

        [Range(0, 200)]
        public decimal? DividendYield { get; set; } = null;



        private decimal? _relativePerformance;
        public decimal? RelativePerformance
        {
            get
            {
                return _relativePerformance;
            }
            set
            {
                _relativePerformance = value;
            }
        }


        private decimal? _absolutePerformance;
        public decimal? AbsolutePerformance
        {
            get
            {
                return _absolutePerformance;
            }
            set
            {
                _absolutePerformance = value;
            }
        }

        public Industry Industry { get; set; }

        #region Polygon API Properties

        [JsonPropertyName("afterHours")]
        public decimal AfterHours { get; set; } = 0;

        [JsonPropertyName("afterHours")]
        public decimal Open { get; set; } = 0;

        [JsonPropertyName("close")] 
        public decimal Close { get; set; } = 0;

        [JsonPropertyName("preMarket")]
        public decimal PreMarket { get; set; } = 0;

        [JsonPropertyName("high")]
        public decimal High { get; set; } = 0;

        [JsonPropertyName("low")]
        public decimal Low { get; set; } = 0;

        #endregion
    }
}
