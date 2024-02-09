namespace PortfolioTrackerShared.Models;

/// <summary>
/// User verification, password change etc.
/// </summary>
public class Email
{
    public string SenderAddress { get; set; } = string.Empty;
    public string RecipientAddress { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
}
