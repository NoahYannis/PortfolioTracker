using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortfolioTrackerShared.Models
{

    public class PortfolioStock
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Portfolio.Id))]
        public int PortfolioId { get; set; }

        [Required(ErrorMessage = "Ticker required."), StringLength(5, MinimumLength = 1, ErrorMessage = "Ticker Must Be Between 1 And 5 Characters")]
        public string Ticker { get; set; } = string.Empty;


        [Column(TypeName = "decimal(18,2)")]
        [Required(ErrorMessage = "Position size required."), Range(0.01f, double.MaxValue, ErrorMessage = "Position Size Must Be Greater Than $0.")]
        public decimal? PositionSize { get; set; } = 0;


        [Column(TypeName = "decimal(18,2)")]
        [Required(ErrorMessage = "Shares owned required."), Range(0.01, double.MaxValue, ErrorMessage = "Shares Owned Must Be Greater Than Zero.")]
        public decimal? SharesOwned { get; set; } = 0;


        [Column(TypeName = "decimal(8,2)")]
        [Required(ErrorMessage = "Buy in price required."), Range(0.1, double.MaxValue, ErrorMessage = "Buy In Price Must Be Greater Than $0.")]
        public decimal? BuyInPrice { get; set; } = 0;


        [Column(TypeName = "decimal(8,2)")]
        private decimal? _currentPrice;
        public decimal? CurrentPrice
        {
            get => PositionSize / SharesOwned;
            set
            {
                if (value is not null && value is not 0)
                    _currentPrice = value;
            }
        }


        [Range(0, 200, ErrorMessage = "Dividend yield must be between 0 and 200%.")]
        public decimal? DividendYield { get; set; }


        [Column(TypeName = "decimal(8,2)")]
        private decimal? _absolutePerformance = 0;
        public decimal? AbsolutePerformance
        {
            get => _absolutePerformance;
            set
            {
                if (CurrentPrice.HasValue && BuyInPrice.HasValue && SharesOwned.HasValue)
                    _absolutePerformance = Math.Round(((CurrentPrice - BuyInPrice) * SharesOwned).Value, 2);
            }
            // Calculates absolute performance and rounds to 2 decimal places
        }


        [Column(TypeName = "decimal(8,2)")]
        private decimal? _relativePerformance = 0;
        public decimal? RelativePerformance
        {
            get => _relativePerformance;
            set
            {
                if (CurrentPrice.HasValue && BuyInPrice.HasValue)
                    _relativePerformance = Math.Round((((CurrentPrice - BuyInPrice) / BuyInPrice) * 100).Value, 2);
            }
            // Calculates relative performance and rounds to 2 decimal places
        }


        public Industry Industry { get; set; }

    }
}
