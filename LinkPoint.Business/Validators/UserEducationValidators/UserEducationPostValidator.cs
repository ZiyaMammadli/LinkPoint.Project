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
        RuleFor(ue => ue.FromDate)
            .LessThanOrEqualTo(DateTime.UtcNow.Year).WithMessage("The value of the FromDate must be down present time")
            .GreaterThan(1920).WithMessage("The value of the FromDate must be up 1920");
        RuleFor(ue => ue.ToDate)
            .LessThanOrEqualTo(DateTime.UtcNow.Year).WithMessage("The value of the ToDate must be down present time")
            .GreaterThan(1920).WithMessage("The value of the ToDate must be up 1920");
        RuleFor(ue => ue.FromDate)
            .Must((ue, fromdate) => fromdate <= ue.ToDate)
            .WithMessage("The value of the FromDate must be down then the value of the ToDate");
        RuleFor(ue => ue.University)
            .MaximumLength(85).WithMessage("The length of the University must be maximum 85");
        RuleFor(ue => ue.Description)
            .MaximumLength(300).WithMessage("The length of the Description must be maximum 300");
    }
}
