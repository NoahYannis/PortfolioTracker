using MimeKit.Text;
using MimeKit;
using MailKit.Security;
using MailKit.Net.Smtp;

namespace PortfolioTrackerServer.Services.EmailService
{
    public class EmailService : IEmailService
    {
        public Task<bool> SendEmail(string body)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("eda.hand12@ethereal.email"));
            email.To.Add(MailboxAddress.Parse("eda.hand12@ethereal.email"));
            email.Subject = "My test email :)";
            email.Body = new TextPart(TextFormat.Plain) { Text = body };
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("eda.hand12@ethereal.email", "v9AhEkDRBEtjkc4RN1");
            smtp.Send(email);
            smtp.Disconnect(true);
            return Task.FromResult(true);
        }
    }
}
