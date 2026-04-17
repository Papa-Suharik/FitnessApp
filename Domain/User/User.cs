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

public class UserProfile
{
    public int Id {get; private set;}
    public int UserId {get; private set;}
    public string Name {get; private set;} = "";
    public int Age {get; private set;}
    public double Height {get; private set;}
    public double Weight {get; private set;}
    public Gender Gender {get; private set;}
    public User User {get; private set;} = null!;
    private UserProfile(){}
    public UserProfile(CreateUserProfileDto dto)
    {
        Name = dto.Name;
        Age = dto.Age;
        Height = dto.Height;
        Weight = dto.Weight;
        Gender = dto.Gender;
    }
}
public class CreateUserProfileDto
{
    public string Name {get; set;} = null!;
    public int Age {get; set;}
    public double Height {get; set;}
    public double Weight {get; set;}
    public Gender Gender {get; set;}
}

public class CreateUserDto
{
    public string Email { get; set; } = null!;
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

public enum Gender
{
    male,
    female,
    notspecified
}