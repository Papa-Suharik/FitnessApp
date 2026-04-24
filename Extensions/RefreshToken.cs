using System.Diagnostics.Eventing.Reader;
using FitnessApp.Domain.User;

namespace FitnessApp.Extensions;

public class RefreshToken
{
    public Guid Id {get; set;}
    public string? HashedToken {get;set;} = null!;
    public int UserId {get; set;}
    public DateTime GeneratedAtUtc {get; set;}
    public DateTime ExpiresOnUtc {get;set;}
    public DateTime? ExpiredOnUtc {get; set;}
    public bool IsRevoked {get; set;} = false;
    public bool IsExpired {get; set;} = false;
    public User? User {get; set;}
}