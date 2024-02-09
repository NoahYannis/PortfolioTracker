namespace PortfolioTrackerShared.Other;

public class EnumDisplayText : Attribute
{
    public string Text { get; }

    public EnumDisplayText(string text)
    {
        Text = text;
    }
}
