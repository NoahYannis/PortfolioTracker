namespace PortfolioTrackerShared.Other;

public class EnumDisplayText(string text) : Attribute
{
    public string Text { get; } = text;
}
