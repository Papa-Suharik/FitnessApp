namespace FitnessApp.Services;

using FitnessApp.Domain.User;

public interface ILoginService
{
    Task<bool> AuthComplete(User user, string password);
    Task<string?> GenerateToken(CreateUserDto dto);
}