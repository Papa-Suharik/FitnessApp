using FitnessApp.Domain.User;
using FluentValidation;

namespace FitnessApp.Validators;

public class CreateuserDtoValidator : AbstractValidator<CreateUserDto>
{
    public CreateuserDtoValidator()
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
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name ir required");

        RuleFor(x => x.Age).NotEmpty().WithMessage("Name is required").ExclusiveBetween(18, 99).WithMessage("Age must be between 18 and 99");

        RuleFor(x => x.Height).NotEmpty().WithMessage("Height is required").ExclusiveBetween(100, 300).WithMessage("Height must be between 100 and 300");

        RuleFor(x => x.Weight).NotEmpty().WithMessage("Weight is required").ExclusiveBetween(30, 300).WithMessage("Weigth must be between 30 and 300");

        RuleFor(x => x.Gender).NotEmpty().WithMessage("Gender is required");
    }
}
