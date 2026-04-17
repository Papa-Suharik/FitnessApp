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
    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _context.Users.Include(u => u.Profile).FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }
    public async Task<User> AddUserAsync(User user, CancellationToken cancellationToken)
    {
        await _context.AddAsync(user, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return user;
    }
    public async Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Users.Include(u => u.Profile).FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
    public async Task Delete(User user, CancellationToken cancellationToken)
    {
        _context.Remove(user);
        await _context.SaveChangesAsync(cancellationToken);
    }
}