using FitnessApp.Domain.User;
using FitnessApp.Extensions;

namespace FitnessApp.Services;

public interface IGlobalTokenHandler
{
    string GenerateToken(User user);
    RefreshToken GenerateRefreshToken(User user, string token);
}