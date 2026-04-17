using FitnessApp.Domain.User;
using FitnessApp.Repo;
using Microsoft.AspNetCore.Identity;

namespace FitnessApp.Services;

public class LoginService : ILoginService
{
    private readonly IJwtProvider _jwtProvider;
    private readonly IUserRepo _userRepo;
    private readonly IPasswordHasher<User> _passwordHasher;
    public LoginService(IJwtProvider jwtProvider, IUserRepo userRepo, IPasswordHasher<User> passwordHasher)
    {
        _jwtProvider = jwtProvider;
        _userRepo = userRepo;
        _passwordHasher = passwordHasher;
    }
    public async Task<bool> AuthComplete(User user, string password)
    {
        var verifed = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);

        if(verifed == PasswordVerificationResult.Failed)
        {
            return false;
        }
        
        return true;
    }

    public async Task<string?> GenerateToken(CreateUserDto dto, CancellationToken cancellationToken)
    {
        var user = await _userRepo.GetByEmailAsync(dto.Email, cancellationToken);

        if(user == null)
        {
            return null;
        }

        if(!await AuthComplete(user, dto.Password))
        {
            return null;
        }

        return _jwtProvider.GenerateToken(user);
    }
}