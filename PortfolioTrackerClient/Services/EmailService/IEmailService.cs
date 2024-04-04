namespace PortfolioTrackerClient.Services.EmailService;

public interface IEmailService
{
    /// <summary>
    /// Send an email to specified adress 
    /// </summary>
    /// <param name="userEmail"></param>
    public Task<bool> SendEmail(string body, string recipientAddress);

}
