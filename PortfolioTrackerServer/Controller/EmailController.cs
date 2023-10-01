using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;

namespace PortfolioTrackerServer.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        [HttpPost]
        public IActionResult SendEmail(string body)
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

            return Ok();
        }
    }
}
