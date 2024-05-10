using FluentValidation;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.UserInterestDTOs;

namespace LinkPoint.Business.Validators.UserInterestValidators;

public class UserInterestDeleteValidator:AbstractValidator<UserInterestDeleteDto>
{
    public UserInterestDeleteValidator()
    {
        RuleFor(ue => ue.Id)
         .NotNull().WithMessage("Id can't be null")
         .NotEmpty().WithMessage("Id can't be empty");
    }
}
