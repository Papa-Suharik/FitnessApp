using FitnessApp.Domain.User;
using FitnessApp.Services;
using Microsoft.AspNetCore.Mvc; 
using FitnessApp.DTOs;

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
    public async Task<IActionResult> Login(LoginUserDto dto, CancellationToken cancellationToken)
    {
        var result = await _loginService.LoginUser(dto, cancellationToken);

        return Ok(result);
    }   
}