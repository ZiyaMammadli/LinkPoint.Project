using FluentValidation;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.ProfileImageDTOs;

namespace LinkPoint.Business.Validators.DefaulImagesValidators;

public class ProfileImageDeleteValidator:AbstractValidator<ProfileImageDeleteDto>
{
    public ProfileImageDeleteValidator()
    {
        RuleFor(p => p.ImageId)
            .NotNull().WithMessage("ImageId can't be null")
            .NotEmpty().WithMessage("ImageId can't be empty");
    }
}
