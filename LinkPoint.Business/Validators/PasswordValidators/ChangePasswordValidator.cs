using FluentValidation;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.PasswordDTOs;

namespace LinkPoint.Business.Validators.PasswordValidators;

public class ChangePasswordValidator:AbstractValidator<ChangePasswordDto>
{
    public ChangePasswordValidator()
    {
        RuleFor(uw => uw.UserId)
            .NotNull().WithMessage("UserId can't be null")
            .NotEmpty().WithMessage("UserId can't be empty");
        RuleFor(p=>p.OldPassword)
            .NotEmpty().WithMessage("Old password can't be empty")
            .NotNull().WithMessage("Old password can't be null")
            .MinimumLength(8).WithMessage("Minimum length of Old Password must be 8");
        RuleFor(p=>p.NewPassword)
            .NotEmpty().WithMessage("New password can't be empty")
            .NotNull().WithMessage("New password can't be null")
            .MinimumLength(8).WithMessage("Minimum length of New Password must be 8"); 
        RuleFor(p=>p.ConfirmNewPassword)
            .NotEmpty().WithMessage("New password can't be empty")
            .NotNull().WithMessage("New password can't be null")
            .MinimumLength(8).WithMessage("Minimum length of New Password must be 8");
        RuleFor(p => p.ConfirmNewPassword)
            .Must((p, confirmNewPassword) => confirmNewPassword == p.NewPassword)
            .WithMessage("ConfirmNewPassword must be same to NewPassword");

    }
}
