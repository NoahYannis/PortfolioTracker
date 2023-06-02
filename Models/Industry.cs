using PortfolioTracker.Other;

namespace PortfolioTracker
{
    public enum Industry
    {
        Technology,
        Healthcare,
        Financial,
        Energy,
        Materials,

        [EnumDisplayText("Real Estate")]
        Real_Estate,

        [EnumDisplayText("Consumer Staples")]
        Consumer_Staples,

        [EnumDisplayText("Consumer Discretionary")]
        Consumer_Discretionary,

        Utilities,
        Industrials,
        Other,
    }
}
