using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RpgGame.Models.DTO;
using RpgGame.Services.Interfaces;

namespace RpgGame.Controllers;

[ApiController]
[Authorize]
[Route("/user")]
public class UserController : Controller
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [Route("me")]
    public async Task<IActionResult> MeAsync()
    {
        UserDTO result = await _userService.GetCurrentUserAsync(User);

        return Ok(result);
    }
}
