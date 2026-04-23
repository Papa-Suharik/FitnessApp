using FitnessApp.Domain.User;
using FluentValidation;

namespace FitnessApp.Validators;

public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format");
        
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long");
    }
}
public class CreateUserProfileDtoValidator : AbstractValidator<CreateUserProfileDto>
{
    public CreateUserProfileDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");

        RuleFor(x => x.Age).InclusiveBetween(5, 99).WithMessage("Age must be between 5 and 99");

        RuleFor(x => x.Height).InclusiveBetween(50, 300).WithMessage("Height must be between 50 and 300");

        RuleFor(x => x.Weight).InclusiveBetween(30, 300).WithMessage("Weight must be between 30 and 300");

        RuleFor(x => x.Gender).IsInEnum().WithMessage("Gender is required");
    }
}
