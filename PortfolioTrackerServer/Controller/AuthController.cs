using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioTrackerServer.Services.AuthService;
using PortfolioTrackerShared.Models.UserModels;
using PortfolioTrackerShared.Other;
using System.Security.Claims;

namespace PortfolioTrackerServer.Controller;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
    private readonly IAuthService _authService = authService;

    [HttpPost("register")]
    public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegister request)
    {
        var response = await _authService.Register(new User { Email = request.Email, UserName = request.Name }, request.Password);

        if (response.Success is false)
            return BadRequest(response);

        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<ActionResult<ServiceResponse<string>>> Login(UserLogin request)
    {
        var response = await _authService.Login(request.Email, request.Password);

        if (response.Success is false)
            return BadRequest(response);

        return Ok(response);
    }

    [HttpGet("user/{email}")]
    public async Task<ActionResult<ServiceResponse<User>>> GetUserFromDbByEmail(string email)
    {
        var response = await _authService.GetUserFromDbByEmail(email);

        if (!response.Success)
            return BadRequest(response);

        return Ok(response);
    }

    [HttpPost("change-password"), Authorize]
    public async Task<ActionResult<ServiceResponse<bool>>> ChangePassword([FromBody] string newPassword)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var response = await _authService.ChangePassword(int.Parse(userId), newPassword);

        if (response.Success is false)
            return BadRequest(response);

        return Ok(response);
    }
}
