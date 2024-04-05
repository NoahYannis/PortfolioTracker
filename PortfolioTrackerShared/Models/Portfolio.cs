using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortfolioTrackerShared.Models;

public class Portfolio
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("UserId")]
    public int UserId { get; set; } = new();

    /// <summary>
    /// All stocks inside the portfolio.
    /// </summary>
    public List<PortfolioStock> Positions { get; set; } = new();

    public decimal PortfolioValue { get; set; } = decimal.Zero;

    /// <summary>
    /// Save as string for the database.
    /// </summary>
    public DateTime DateCreated { get; set; } = DateTime.Today;

}
