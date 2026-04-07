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
    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [Required]
    public string? Name { get; set; }

    [Required]
    public int Age { get; set; }

    [Required]
    public double Height { get; set; }

    [Required]
    public double Weight { get; set; }

    [Required]
    public Gender Gender { get; set; }

    public UserDto(User user)
    {
        Email = user.Email;
        Name = user.Profile!.Name;
        Age = user.Profile!.Age;
        Height = user.Profile.Height;
        Weight = user.Profile.Weight;
        Gender = user.Profile.Gender;
    }
}