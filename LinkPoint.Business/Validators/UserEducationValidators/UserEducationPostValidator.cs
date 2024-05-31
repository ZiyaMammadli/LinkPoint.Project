using FluentValidation;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.UserEducationDTOs;

namespace LinkPoint.Business.Validators.UserEducationValidators;

public class UserEducationPostValidator:AbstractValidator<UserEducationPostDto>
{
    public UserEducationPostValidator()
    {
        RuleFor(ue => ue.UserId)
          .NotNull().WithMessage("UserId can't be null")
          .NotEmpty().WithMessage("UserId can't be empty");
        RuleFor(uw => uw.FromDate)
            .Must(fromDate => fromDate == null || (fromDate <= DateTime.UtcNow.Year && fromDate > 1920))
            .WithMessage("The value of the FromDate must be either null or between 1920 and the present time.");
        RuleFor(uw => uw.ToDate)
            .Must(toDate => toDate == null || (toDate <= DateTime.UtcNow.Year && toDate > 1920))
            .WithMessage("The value of the ToDate must be either null or between 1920 and the present time.");
        RuleFor(uw => uw.FromDate)
            .Must((uw, fromDate) =>
                fromDate == null || uw.ToDate == null || fromDate <= uw.ToDate)
            .WithMessage("The value of the FromDate must be less than or equal to the value of the ToDate when both dates are provided.");
        RuleFor(ue => ue.University)
            .MaximumLength(85).WithMessage("The length of the University must be maximum 85");
        RuleFor(ue => ue.Description)
            .MaximumLength(300).WithMessage("The length of the Description must be maximum 300");
    }
}
