using FitnessApp.Domain.User;

namespace FitnessApp.DTOs;

public class LoginUserDto
{
    public string Email {get; set;}
    public string Password {get; set;}
}
public class AuthResultDto
{
    public int Id {get; set;}
    public string JwtToken {get; set;} = null!;
    public string RefreshToken {get; set;} = null!;
}