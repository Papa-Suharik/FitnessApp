using System.Collections;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using FitnessApp.Domain.User;
using FitnessApp.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace FitnessApp.Services;

public class GlobalTokenHandler(IConfiguration configuration, IPasswordHasher<User> hasher) : IGlobalTokenHandler
{
    public string GenerateToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
          new Claim(JwtRegisteredClaimNames.Email, user.Email)
          ]),
            Expires = DateTime.UtcNow.AddMinutes(configuration.GetValue<double>("Jwt:ExpirationMinutes")),
            Issuer = configuration["Jwt:Issuer"],
            Audience = configuration["Jwt:Audience"],
            SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
        };

        var handler = new JsonWebTokenHandler();

        var key = handler.CreateToken(tokenDescriptor);

        return key;
    }

    public RefreshToken GenerateRefreshToken(User user, string token)
    {
        return new RefreshToken
        {
            Id = Guid.NewGuid(),
            HashedToken = hasher.HashPassword(user, token),
            UserId = user.Id,
            GeneratedAtUtc = DateTime.UtcNow,
            ExpiresOnUtc = DateTime.UtcNow.AddDays(7),
        };
    }
}