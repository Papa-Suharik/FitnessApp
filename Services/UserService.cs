using FitnessApp.Domain.User;
using Microsoft.AspNetCore.Identity;
using FitnessApp.Repo;
using System.Data;
using System.Linq.Expressions;
using System.Xml;
using Microsoft.AspNetCore.Http.HttpResults;
using FitnessApp.CustomMiddleware;
using Microsoft.VisualBasic;

namespace FitnessApp.Services;

public class UserService : IUserService
{
    private readonly IPasswordHasher<User> _passwordHasher;  
    private readonly IUserRepo _repo;
    private readonly IJwtProvider _jwtProvider;
    public UserService(IPasswordHasher<User> passwordHasher, IUserRepo repo, IJwtProvider jwtProvider)
    {
        _passwordHasher = passwordHasher;
        _repo = repo;
        _jwtProvider = jwtProvider;
    }
    public async Task<UserDto> CreateAsync(CreateUserDto dto)
    {
        var result = await _repo.GetByEmailAsync(dto.Email!);

        if(result != null)
        {
            throw new DuplicateUserException("User already exist!");
        }

        var user = new User(dto.Email, "");
        user.ChangePassword(_passwordHasher.HashPassword(user, dto.Password));

        await _repo.AddUserAsync(user);

        var userDto = new UserDto(user);

        return userDto;
    }

    public async Task<UserDto?> ProfileSetupAsync(int id, CreateUserProfileDto dto)
    {
        var user = await _repo.GetByIdAsync(id);

        if(user == null)
        {
            throw new UserNotFoundException("No user found!");
        }

        user.SetUserProfile(dto);

        await _repo.SaveChangesAsync();

        return new UserDto(user);
    }

    public async Task<string> LoginAsync(CreateUserDto dto)
    {
        var user = await _repo.GetByEmailAsync(dto.Email) ?? throw new Exception("No user found!");

        var verified = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

        if(verified == PasswordVerificationResult.Failed)
        {
            throw new Exception("Password is incorrect");
        }

        string token = _jwtProvider.GenerateToken(user);

        return token;
    }

    public async Task<UserDto> GetByIdAsync(int id)
    {
        var user = await _repo.GetByIdAsync(id);

        if(user == null)
        {
            throw new UserNotFoundException("No user found!");
        }

        var dto = new UserDto(user);
        
        return dto;
    }
    public async Task DeleteUser(int id)
    {
        var user = await _repo.GetByIdAsync(id);

        if(user != null)
        {
            await _repo.Delete(user);
        }
    }
}

