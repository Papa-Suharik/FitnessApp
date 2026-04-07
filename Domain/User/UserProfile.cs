namespace FitnessApp.Domain.User;
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
    public string Name {get; set;} = "";
    public int Age {get; set;}
    public double Height {get; set;}
    public double Weight {get; set;}
    public Gender Gender {get; set;}
}