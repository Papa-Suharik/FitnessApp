namespace FitnessApp.Services;

using FitnessApp.Domain;
using FitnessApp.Domain.User;

public interface IUserService
{
    Task<User> CreateAsync(CreateUserDto dto);
    Task<UserDto?> ProfileSetupAsync(int id, CreateUserProfileDto dto);
    Task<string> LoginAsync(CreateUserDto dto);
    Task<UserDto> GetByIdAsync(int id);
    Task DeleteUser(int id);
}