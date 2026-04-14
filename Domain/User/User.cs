namespace FitnessApp.Domain.User;

using FitnessApp.Domain.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

public class User
{
    public int Id { get; private set; }
    public string Email { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;
    public UserProfile? Profile { get; private set; }
    private User() { }
    public User(string email, string passwordHash)
    {
        Email = email;
        PasswordHash = passwordHash;
    }
    public void ChangePassword(string newHash)
    {
        PasswordHash = newHash;
    }
    public void SetUserProfile(CreateUserProfileDto dto)
    {
        Profile = new UserProfile(dto);
    }
}

public class CreateUserDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}
public class UserDto
{
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public string? Name { get; set; }
    public int? Age { get; set; }
    public double? Height { get; set; }
    public double? Weight { get; set; }
    public Gender? Gender { get; set; }
}