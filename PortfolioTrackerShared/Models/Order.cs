using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortfolioTrackerShared.Models
{
    public enum OrderType
    {
        Buy,
        Sell,
    }

    public class Order
    {

        [Key]
        public int OrderNumber { get; set; }


        /// <summary>
        /// The order stock's ticker symbol
        /// </summary>
        [ForeignKey(nameof(Ticker))]
        public string Ticker { get; set; } = string.Empty;


        [ForeignKey(nameof(UserId))]
        public int UserId { get; set; }

        public OrderType OrderType { get; set; }
        public DateTime Date { get; set; }
        public int Quantity { get; set; } = 0;
        public decimal Price { get; set; } = decimal.Zero;
    }
}
