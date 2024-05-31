using FluentValidation;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.UserWorkDTOs;

namespace LinkPoint.Business.Validators.UserWorkValidators;

public class UserWorkPostValidator:AbstractValidator<UserWorkPostDto>
{
    public UserWorkPostValidator()
    {
        RuleFor(uw => uw.UserId)
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
        RuleFor(uw => uw.Company)
            .MaximumLength(85).WithMessage("The length of the Company must be maximum 85");
        RuleFor(uw => uw.Designation)
            .MaximumLength(85).WithMessage("The length of the Designation must be maximum 85");
        RuleFor(uw => uw.Description)
            .MaximumLength(300).WithMessage("The length of the Description must be maximum 300");
    }
}
