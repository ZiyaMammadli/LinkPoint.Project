using FluentValidation;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.UserAboutDTOs;

namespace LinkPoint.Business.Validators.UserAboutValidators;

public class UserAboutPutValidator : AbstractValidator<UserAboutPutDto>
{
    public UserAboutPutValidator()
    {
        RuleFor(ua => ua.UserId)
            .NotNull().WithMessage("UserId can't be null")
            .NotEmpty().WithMessage("UserId can't be empty");
        RuleFor(ua => ua.AboutMe)
            .MaximumLength(300).WithMessage("The length of the AboutMe must be maximum 300");
        RuleFor(ua => ua.CityName)
            .MaximumLength(85).WithMessage("The length of the CityName must be maximum 85");
        RuleFor(ua => ua.CountryName)
            .MaximumLength(85).WithMessage("The length of the CountryName must be maximum 85");
        RuleFor(ua => ua.Male)
            .Must((ua, male) => male != ua.Female)
            .WithMessage("The values of Male and Female can't be same");
    }
}
