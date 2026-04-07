using System.Reflection.Metadata.Ecma335;
using FitnessApp.Data;
using FitnessApp.Domain.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Repo;

public class UserRepo : IUserRepo
{
    private readonly ApplicationContext _context;
    public UserRepo(ApplicationContext context)
    {
        _context = context;
    }
    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users.Include(u => u.Profile).FirstOrDefaultAsync(u => u.Email == email);
    }
    public async Task<User> AddUserAsync(User user)
    {
        await _context.AddAsync(user);

        await _context.SaveChangesAsync();

        return user;
    }
    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users.Include(u => u.Profile).FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}