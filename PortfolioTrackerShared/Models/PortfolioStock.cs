﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PortfolioTrackerShared.Models
{

    public class PortfolioStock
    {
        [Key]
        [Required(ErrorMessage = "Ticker required."), StringLength(5, MinimumLength = 1, ErrorMessage = "Ticker Must Be Between 1 And 5 Characters")]
        public string Ticker { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        [Required(ErrorMessage = "Position size required."), Range(0.01f, double.MaxValue, ErrorMessage = "Position Size Must Be Greater Than $0.")]
        public decimal? PositionSize { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Required(ErrorMessage = "Shares owned required."), Range(0.01, double.MaxValue, ErrorMessage = "Shares Owned Must Be Greater Than Zero.")]
        public decimal? SharesOwned { get; set; }

        [Column(TypeName = "decimal(8,2)")]
        [Required(ErrorMessage = "Buy in price required."), Range(0.1, double.MaxValue, ErrorMessage = "Buy In Price Must Be Greater Than $0.")]
        public decimal? BuyInPrice { get; set; }

        [Range(0, 200, ErrorMessage = "Dividend yield must be between 0 and 200%.")]
        public decimal? DividendYield { get; set; }


        [Column(TypeName = "decimal(8,2)")]
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


        [Column(TypeName = "decimal(8,2)")]
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

    }
}