using FitnessApp.Domain.User;

namespace FitnessApp.Extensions;

public static class Mapper
{
    public static UserDto ToDto(this User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            Name = user.Profile?.Name,
            Age = user.Profile?.Age,
            Height = user.Profile?.Height,
            Weight = user.Profile?.Weight,
            Gender = user.Profile?.Gender
        };
    }
}