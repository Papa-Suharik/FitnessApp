using FitnessApp.Domain.User;

namespace FitnessApp.Services;

public interface IJwtProvider
{
    string GenerateToken(User user);
}