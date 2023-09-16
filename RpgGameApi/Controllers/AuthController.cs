using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RpgGame.Misc;
using RpgGame.Models.DTO;
using RpgGame.Models.Request;
using RpgGame.Services.Interfaces;

namespace RpgGame.Controllers;

[ApiController]
[Route("/auth")]
public class AuthController : Controller
{
	private readonly IAuthService _authService;

	public AuthController(IAuthService authService)
	{
		_authService = authService;
	}

    [HttpPatch]
    [Route("confirm-user")]
    public async Task<IActionResult> ConfirmUserAsync(ulong? id, string? email, string? username)
    {
        if (!id.HasValue || id == 0)
            return BadRequest("Id cannot be empty or null.");

        if (string.IsNullOrWhiteSpace(email))
            return BadRequest("Email cannot be empty or null.");

        if (string.IsNullOrWhiteSpace(username))
            return BadRequest("Username cannot be empty or null.");

        SuccessDTO result = await _authService.ConfirmUserAsync(id.Value, email, username);

        return Ok(result);
    }

    [HttpGet]
    [Route("is-confirmed-user")]
    public async Task<IActionResult> IsConfirmedUserAsync(ulong? id, string? email, string? username)
    {
        if (!id.HasValue || id == 0)
            return BadRequest("Id cannot be empty or null.");

        if (string.IsNullOrWhiteSpace(email))
            return BadRequest("Email cannot be empty or null.");

        if (string.IsNullOrWhiteSpace(username))
            return BadRequest("Username cannot be empty or null.");

        IsUserConfirmedDTO result = await _authService.IsUserConfirmedAsync(id.Value, email, username);

        return Ok(result);
    }

    [HttpPost]
    [Route("login")]
	public async Task<IActionResult> LoginAsync([FromBody] LoginUserRequest? body)
	{
        if (body is null)
            return BadRequest("Model cannot be null.");

        if (!ModelState.IsValid) 
            return BadRequest(ModelState.GetErrors());

        TokenDTO result = await _authService.LoginAsync(body);

		return Ok(result);
	}

    [HttpPost]
    [Route("register")]
	public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserRequest? body)
	{
        if (body is null)
            return BadRequest("Model cannot be null.");

        if (!ModelState.IsValid) 
            return BadRequest(ModelState.GetErrors());

        RegisterUserDTO result =  await _authService.RegisterAsync(body);		

		return CreatedAtRoute("/user/me", result);
	}
	
	[Authorize]
	[Route("renew-token")]
	[HttpPost]
	public async Task<IActionResult> RenewTokenAsync([FromBody] RenewTokenRequest? body)
	{
        if(body is null)
            return BadRequest("Model cannot be null.");

        if (!ModelState.IsValid) 
            return BadRequest(ModelState.GetErrors());

        TokenDTO result = await _authService.RenewTokenAsync(User, body);

        return Ok(result);
	}

    [AllowAnonymous]
    [HttpPost]
    [Route("resend-confirmation-email")]
    public async Task<IActionResult> ResendConfirmationMailAsync(ulong? id, string? email, string? username)
    {
        if (!id.HasValue || id == 0)
            return BadRequest("Id cannot be empty or null.");

        if (string.IsNullOrWhiteSpace(email))
            return BadRequest("Email cannot be empty or null.");

        if (string.IsNullOrWhiteSpace(username))
            return BadRequest("Username cannot be empty or null.");

        SuccessDTO result = await _authService.ResendConfirmationMailAsync(id.Value, email, username);

        return Ok(result);
    }
}
