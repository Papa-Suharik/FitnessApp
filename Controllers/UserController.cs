using Microsoft.AspNetCore.Mvc;
using FitnessApp.Domain;
using FitnessApp.Services;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using FitnessApp.Domain.User;
using Microsoft.AspNetCore.Authorization;
using FitnessApp.CustomMiddleware;

namespace FitnessApp.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
    {
        var user = await _userService.CreateAsync(dto);

        return CreatedAtAction(nameof(GetById), new {id = user.Id});
    }

    [HttpPatch("profile/{id}")]
    public async Task<IActionResult> Profile(int id, [FromBody] CreateUserProfileDto dto)
    {
        var user = await _userService.ProfileSetupAsync(id, dto);

        if(user == null)
        {
            return NotFound("User hasn't been found");
        }

        return Ok(user);
    }

    // [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _userService.GetByIdAsync(id);

        if(user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        await _userService.DeleteUser(id);

        return NoContent();
    }

}