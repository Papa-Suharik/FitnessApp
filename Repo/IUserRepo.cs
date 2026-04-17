using FitnessApp.Domain.User;

namespace FitnessApp.Repo;

public interface IUserRepo
{
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task<User> AddUserAsync(User user, CancellationToken cancellationToken);
    Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
    Task Delete(User user, CancellationToken cancellationToken);
}