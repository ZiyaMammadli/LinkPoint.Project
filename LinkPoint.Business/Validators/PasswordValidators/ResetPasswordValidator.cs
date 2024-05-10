using FluentValidation;
using LinkPoint.Business.DTOs.AccountDTOs;

namespace LinkPoint.Business.Validators.PasswordValidators;

public class ResetPasswordValidator:AbstractValidator<ResetPasswordDto>
{
    public ResetPasswordValidator()
    {
        RuleFor(p=>p.Token)
           .NotEmpty().WithMessage("Token can't be empty")
           .NotNull().WithMessage("Token can't be null");
        RuleFor(p => p.NewPassword)
           .NotEmpty().WithMessage("New password can't be empty")
           .NotNull().WithMessage("New password can't be null")
           .MinimumLength(8).WithMessage("Minimum length of New Password must be 8");
    }
}
