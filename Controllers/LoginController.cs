using FitnessApp.Domain.User;
using FitnessApp.Services;
using Microsoft.AspNetCore.Mvc; 

namespace FitnessApp.Controllers;

[ApiController]
[Route("api/login")]
public class LoginHandler : ControllerBase
{
    private readonly ILoginService _loginService;
    public LoginHandler(ILoginService loginService)
    {
        _loginService = loginService;
    }

    [HttpPost]
    public async Task<IActionResult> Login(CreateUserDto dto, CancellationToken cancellationToken)
    {
        string? token = await _loginService.GenerateToken(dto, cancellationToken);

        if(token == null)
        {
            return BadRequest("Something went wrong");
        }

        return Ok(token);
    }   
}