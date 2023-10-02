using Microsoft.AspNetCore.Mvc;
using PortfolioTrackerServer.Services.EmailService;

namespace PortfolioTrackerServer.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> SendEmail(string body)
        {
            var result = await _emailService.SendEmail(body);
            return Ok(result);
        }
    }
}
