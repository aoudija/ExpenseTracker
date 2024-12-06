namespace backendApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using backendApi.Services;
using backendApi.Models;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpGet("test")]
    public IActionResult Test()
    {
        return Ok("UserController is working!");
    }
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        Console.WriteLine("***********UserController is initialized.********");
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] User user)
    {
        var createdUser = await _userService.RegisterUser(user);
        return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(int id)
    {
        var user = await _userService.GetUserById(id);
        if (user == null) return NotFound();
        return Ok(user);
    }
}
