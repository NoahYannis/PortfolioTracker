using Microsoft.AspNetCore.Mvc;
using PortfolioTrackerServer.Services.EmailService;

namespace PortfolioTrackerServer.Controller;

[Route("api/[controller]")]
[ApiController]
public class EmailController(IEmailService emailService) : ControllerBase
{
    private readonly IEmailService _emailService = emailService;

    [HttpPost]
    public async Task<ActionResult> SendEmail([FromBody] string body, string recipientAddress)
    {
        await _emailService.SendEmail(body, recipientAddress);
        return Ok();
    }
}
