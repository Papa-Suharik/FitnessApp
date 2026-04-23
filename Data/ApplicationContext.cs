using FitnessApp.Domain.User;
using FitnessApp.Extensions;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Data;

public class ApplicationContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<UserProfile> UserProfiles => Set<UserProfile>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
           entity.ToTable("Users");
           entity.HasKey(e => e.Id); 
           entity.Property(e => e.Id).ValueGeneratedOnAdd();
           entity.Property(e => e.Email).IsRequired().HasMaxLength(256);
           entity.HasIndex(e =>e.Email).IsUnique();
           entity.Property(e => e.PasswordHash).IsRequired().HasMaxLength(256);
           entity.HasOne(e => e.Profile).WithOne(e => e.User).HasForeignKey<UserProfile>(e => e.UserId).OnDelete(DeleteBehavior.Cascade);
           entity.HasMany(e => e.RefreshTokens).WithOne(e => e.User).HasForeignKey(e => e.UserId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<UserProfile>(entity =>
        {
           entity.ToTable("UserProfiles");
           entity.HasKey(e => e.Id);
           entity.Property(e => e.Id).ValueGeneratedOnAdd();
           entity.Property(e => e.UserId).IsRequired();
           entity.HasIndex(e => e.UserId).IsUnique();
           entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
           entity.Property(e => e.Age).IsRequired();
           entity.Property(e => e.Height).IsRequired().HasPrecision(5, 2);
           entity.Property(e => e.Weight).IsRequired().HasPrecision(5, 2);
           entity.Property(e => e.Gender).IsRequired();
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.ToTable("RefreshTokens");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).IsRequired();
            entity.Property(e => e.Token).IsRequired().HasMaxLength(200);
            entity.HasIndex(e => e.Token).IsUnique();
            entity.Property(e => e.ExpiresOnUtc).IsRequired();
            entity.Property(e => e.UserId).IsRequired();
        });
    }
}  