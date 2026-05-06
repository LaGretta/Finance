using FinanceAPI.DTOs;
using FluentValidation;

namespace FinanceAPI.Validators;

public class RegisterValidator : AbstractValidator<AuthDtos.RegisterDto>
{
    public RegisterValidator()
    {
        RuleFor(n => n.Username)
            .NotEmpty()
            .WithMessage("Username is required")
            .MinimumLength(3)
            .WithMessage("Username must be at least 3 characters long")
            .MaximumLength(50)
            .WithMessage("Username must be no more than 30 characters long");
        
        
        RuleFor(n => n.Password)
            .NotEmpty()
            .WithMessage("Password is required")
            .MinimumLength(6)
            .WithMessage("Password must be at least 6 characters long");
    }
}