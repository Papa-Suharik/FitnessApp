using Microsoft.AspNetCore.Mvc;
using FitnessApp.Domain;
using FitnessApp.Services;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using FitnessApp.Domain.User;
using Microsoft.AspNetCore.Authorization;

namespace FitnessApp.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserDto dto, CancellationToken cancellationToken)
    {
        var user = await _userService.CreateAsync(dto, cancellationToken);

        return CreatedAtAction(nameof(GetById), new {id = user.Id}, user);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateProfile(int id, [FromBody] CreateUserProfileDto dto, CancellationToken cancellationToken)
    {
        var user = await _userService.ProfileSetupAsync(id, dto, cancellationToken);

        return Ok(user);
    }

    // [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var user = await _userService.GetByIdAsync(id, cancellationToken);

        return Ok(user);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await _userService.DeleteUser(id, cancellationToken);

        return NoContent();
    }

}