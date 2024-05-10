using FluentValidation;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.UserInterestDTOs;

namespace LinkPoint.Business.Validators.UserInterestValidators;

public class UserInterestPutValidator:AbstractValidator<UserInterestPutDto>
{
    public UserInterestPutValidator()
    {
        RuleFor(ui => ui.UserId)
        .NotNull().WithMessage("UserId can't be null")
        .NotEmpty().WithMessage("UserId can't be empty");
        RuleFor(ue => ue.Interest)
            .MaximumLength(85).WithMessage("The length of the Interest must be maximum 85");
    }
}
