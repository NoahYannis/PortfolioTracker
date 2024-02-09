using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace PortfolioTrackerServer.Services.EmailService;

public class EmailService : IEmailService
{
    private readonly IConfiguration config;

    public EmailService(IConfiguration _config)
    {
        config = _config;
    }

    public Task SendEmail(string body, string recipientAddress)
    {
        var email = new MimeMessage();

        email.From.Add(MailboxAddress.Parse(config.GetSection("Email:AppMail").Value));
        email.To.Add(MailboxAddress.Parse(recipientAddress));
        email.Subject = "Email Subject";
        email.Body = new TextPart(TextFormat.Plain) { Text = body };

        using var smtp = new SmtpClient();

        smtp.Connect(config.GetSection("Email:Host").Value, 587, SecureSocketOptions.StartTls);
        smtp.Authenticate(config.GetSection("Email:AppMail").Value, config.GetSection("Email:Password").Value);
        smtp.Send(email);
        smtp.Disconnect(true);

        return Task.FromResult(true);
    }
}
