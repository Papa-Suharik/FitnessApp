namespace FitnessApp.Services;

using FitnessApp.Domain.User;
using FitnessApp.DTOs;

public interface ILoginService
{
    Task<AuthResultDto> LoginUser(LoginUserDto dto, CancellationToken token);
}