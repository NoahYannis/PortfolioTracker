using PortfolioTracker.Other;
using System.ComponentModel.DataAnnotations;

namespace PortfolioTracker
{
    public enum Industry
    {
        Technology,
        Healthcare,
        Financial,
        Energy,
        Materials,

        // UpdateStockComponent validation fails when display text contains spaces

        [EnumDisplayText("Real_Estate")]
        Real_Estate,

        [EnumDisplayText("Consumer_Staples")]
        Consumer_Staples,

        [EnumDisplayText("Consumer_Discretionary")]
        Consumer_Discretionary,

        Utilities,
        Industrials,
        Other,
    }
}
