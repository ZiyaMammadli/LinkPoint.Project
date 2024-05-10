using FluentValidation;
using LinkPoint.Business.DTOs.AccountDTOs;

namespace LinkPoint.Business.Validators.PasswordValidators;

public class ForgotPasswordValidator:AbstractValidator<ForgotPasswordDto>
{
    public ForgotPasswordValidator()
    {
        RuleFor(p=>p.Email)
            .NotEmpty().WithMessage("Email can't be empty")
            .NotNull().WithMessage("Email can't be null")
            .EmailAddress().WithMessage("Only Email must entered")
            .MaximumLength(140).WithMessage("Maximum length of Email must be 140");
    }
}
