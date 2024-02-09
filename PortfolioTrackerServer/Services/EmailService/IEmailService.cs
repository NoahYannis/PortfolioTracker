namespace PortfolioTrackerServer.Services.EmailService;

public interface IEmailService
{
    public Task SendEmail(string body, string recipientAddress);
}
