using FluentValidation;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.ProfileImageDTOs;

namespace LinkPoint.Business.Validators.DefaulImagesValidators;

public class ProfileImagePutValidator:AbstractValidator<ProfileImagePutDto>
{
    public ProfileImagePutValidator()
    {
        RuleFor(p => p.ImageId)
            .NotNull().WithMessage("ImageId can't be null")
            .NotEmpty().WithMessage("ImageId can't be empty");
        RuleFor(p => p.ProfileImage)
           .NotNull().WithMessage("ProfileImage can't be null")
           .Must(p => p.Length > 0).WithMessage("ProfileImage can't be empty")
           .Must(p => p.ContentType == "image/png" || p.ContentType == "image/jpeg")
           .WithMessage("Image must be jpg/png")
           .Must(p => p.Length <= 15 * 1024 * 1024)
           .WithMessage("Image size must be lower than 15 mb");
    }
}
