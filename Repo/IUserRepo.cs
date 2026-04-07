using FitnessApp.Domain.User;

namespace FitnessApp.Repo;

public interface IUserRepo
{
    Task<User?> GetByEmailAsync(string email);
    Task<User> AddUserAsync(User user);
    Task<User?> GetByIdAsync(int id);
    Task SaveChangesAsync();
}