namespace FitnessApp.Services;

using FitnessApp.Domain;
using FitnessApp.Domain.User;

public interface IUserService
{
    public Task<User> CreateAsync(CreateUserDto dto);
    public Task<UserDto?> ProfileSetupAsync(int id, CreateUserProfileDto dto);
    public Task<string> LoginAsync(CreateUserDto dto);
    public Task<UserDto> GetByIdAsync(int id);
}    