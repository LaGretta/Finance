using FinanceAPI.DTOs;
using FluentValidation;

namespace FinanceAPI.Validators;

public class CreateExpenseValidator : AbstractValidator<ExpenseDtos.CreateExpenseDto>
{
    public CreateExpenseValidator()
    {
        RuleFor(e => e.Title)
            .NotEmpty()
            .WithMessage("Title is required")
            .MaximumLength(100);
        
        RuleFor(e => e.Category)
            .NotEmpty()
            .WithMessage("Category is required")
            .MaximumLength(50);
        
        RuleFor(e => e.Amount)
            .GreaterThan(0)
            .WithMessage("Amount must be greater than 0");
    }
}