namespace FitnessApp.Services;

using FitnessApp.Domain;
using FitnessApp.Domain.User;

public interface IUserService
{
    Task<UserDto> CreateAsync(CreateUserDto dto, CancellationToken cancellationToken);
    Task<UserDto?> ProfileSetupAsync(int id, CreateUserProfileDto dto, CancellationToken cancellationToken);
    Task<string> LoginAsync(CreateUserDto dto, CancellationToken cancellationToken);
    Task<UserDto> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task DeleteUser(int id, CancellationToken cancellationToken);
}