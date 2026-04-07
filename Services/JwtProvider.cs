using System.Security.Claims;
using System.Text;
using FitnessApp.Domain.User;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace FitnessApp.Services;

public class JwtProvider(IConfiguration configuration) : IJwtProvider
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
}