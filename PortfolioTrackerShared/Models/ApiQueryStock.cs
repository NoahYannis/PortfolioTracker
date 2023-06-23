using System.Text.Json.Serialization;

namespace PortfolioTrackerShared.Models
{
    /// <summary>
    /// The data retrieved from the Polygon.io API call.
    /// </summary>
    public class ApiQueryStock
    {
        public string Ticker { get; set; } = string.Empty;

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
