using System.Security.Cryptography;
using FitnessApp.CustomExceptions;
using FitnessApp.Domain.User;
using FitnessApp.DTOs;
using FitnessApp.Repo;
using Microsoft.AspNetCore.Identity;

namespace FitnessApp.Services;

public class LoginService : ILoginService
{
    private readonly IGlobalTokenHandler _globalTokenHandler;
    private readonly IUserRepo _userRepo;
    private readonly IPasswordHasher<User> _passwordHasher;
    public LoginService(IGlobalTokenHandler globalTokenHandler, IUserRepo userRepo, IPasswordHasher<User> passwordHasher)
    {
        _globalTokenHandler = globalTokenHandler;
        _userRepo = userRepo;
        _passwordHasher = passwordHasher;
    }
    public async Task<AuthResultDto> LoginUser(LoginUserDto dto, CancellationToken cancellationToken)
    {
       var user = await _userRepo.GetByEmailAsync(dto.Email, cancellationToken) ?? throw new AuthenticationFailedException("The credentials entered don't match our records. Please try again or reset your password.");
       
       var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

       if(result is PasswordVerificationResult.Failed)
        {
            throw new AuthenticationFailedException("The credentials entered don't match our records. Please try again or reset your password.");
        }

        var jwtToken = _globalTokenHandler.GenerateToken(user);
        var rawToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        var refreshToken = _globalTokenHandler.GenerateRefreshToken(user, rawToken);

        await _userRepo.AddRefreshToken(refreshToken, cancellationToken);

        await _userRepo.SaveChangesAsync(cancellationToken);

        return new AuthResultDto
        {
            JwtToken = jwtToken,
            RefreshToken = rawToken
        };
    }
}