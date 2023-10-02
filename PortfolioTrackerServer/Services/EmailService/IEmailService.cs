namespace PortfolioTrackerServer.Services.EmailService
{
    public interface IEmailService
    {
        public Task<bool> SendEmail(string body);
    }
}
