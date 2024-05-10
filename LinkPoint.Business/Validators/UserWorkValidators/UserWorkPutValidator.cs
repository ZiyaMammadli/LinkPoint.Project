using FluentValidation;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.UserWorkDTOs;

namespace LinkPoint.Business.Validators.UserWorkValidators;

public class UserWorkPutValidator:AbstractValidator<UserWorkPutDto>
{
    public UserWorkPutValidator()
    {
        RuleFor(uw => uw.UserId)
           .NotNull().WithMessage("UserId can't be null")
           .NotEmpty().WithMessage("UserId can't be empty");
        RuleFor(uw => uw.Id)
           .NotNull().WithMessage("UserId can't be null")
           .NotEmpty().WithMessage("UserId can't be empty");
        RuleFor(uw => uw.FromDate)
            .LessThanOrEqualTo(DateTime.UtcNow.Year).WithMessage("The value of the FromDate must be down present time")
            .GreaterThan(1920).WithMessage("The value of the FromDate must be up 1920");
        RuleFor(uw => uw.ToDate)
            .LessThanOrEqualTo(DateTime.UtcNow.Year).WithMessage("The value of the ToDate must be down present time")
            .GreaterThan(1920).WithMessage("The value of the ToDate must be up 1920");
        RuleFor(uw => uw.FromDate)
            .Must((uw, fromdate) => fromdate <= uw.ToDate)
            .WithMessage("The value of the FromDate must be down then the value of the ToDate");
        RuleFor(uw => uw.Company)
            .MaximumLength(85).WithMessage("The length of the Company must be maximum 85");  
        RuleFor(uw => uw.Designation)
            .MaximumLength(85).WithMessage("The length of the Designation must be maximum 85");     
        RuleFor(uw => uw.Description)
            .MaximumLength(300).WithMessage("The length of the Description must be maximum 300");
    }
}
