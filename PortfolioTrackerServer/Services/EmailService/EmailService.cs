using MimeKit.Text;
using MimeKit;
using MailKit.Security;
using MailKit.Net.Smtp;

namespace PortfolioTrackerServer.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration config;

        public EmailService(IConfiguration _config)
        {
            config = _config;
        }

        public Task<bool> SendEmail(string body, string recipientAddress)
        {
            var email = new MimeMessage();

            email.From.Add(MailboxAddress.Parse(config.GetSection("Email:AppMail").Value));
            email.To.Add(MailboxAddress.Parse(recipientAddress));
            email.Subject = "My test email :)";
            email.Body = new TextPart(TextFormat.Plain) { Text = body };

            using var smtp = new SmtpClient();

            smtp.Connect(config.GetSection("Email:Host").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(config.GetSection("Email:AppMail").Value, config.GetSection("Email:Password").Value);
            smtp.Send(email);
            smtp.Disconnect(true);

            return Task.FromResult(true);
        }

        public Task<bool> SendPasswordResetEmail(string userEmail)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("portfoliotracker.support@reset.email"));
            email.To.Add(MailboxAddress.Parse(userEmail));
            email.Subject = "Resetting your PortfolioTracker password";

            email.Body = new TextPart(TextFormat.Html) 
            { 
                Text =  "<h1>Reset your password for PortfolioTracker</h1>\r\n<p>\r\n" +
                "To reset your password, click the link:\r\n</p>\r\n<p>\r\n" +
                "<a href=\"[https://localhost:7207/api/email/change-password]\">Reset password</a>\r\n" +
                "</p>\r\n<p>\r\n" 
            };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(userEmail, "qT3K7TEMFswEV7pp87");
            smtp.Send(email);
            smtp.Disconnect(true);
            return Task.FromResult(true);
        }
    }
}
