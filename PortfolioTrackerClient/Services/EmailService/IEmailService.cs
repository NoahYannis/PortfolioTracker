namespace PortfolioTrackerClient.Services.EmailService
{
    public interface IEmailService
    {
        public Task<bool> SendEmail(string body);

        /// <summary>
        /// Send an email to specified adress 
        /// </summary>
        /// <param name="userEmail"></param>
        /// <returns>Whether the email was sent succesfully</returns>
        public Task<bool> SendPasswordResetEmail(string userEmail);
    }
}
