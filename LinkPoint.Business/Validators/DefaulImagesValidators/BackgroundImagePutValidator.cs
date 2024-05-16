using FluentValidation;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.BackgroundImageDTOs;

namespace LinkPoint.Business.Validators.DefaulImagesValidators;

public class BackgroundImagePutValidator:AbstractValidator<BackgroundImagePutDto>
{
    public BackgroundImagePutValidator()
    {
        RuleFor(p => p.ImageId)
            .NotNull().WithMessage("ImageId can't be null")
            .NotEmpty().WithMessage("ImageId can't be empty");
        RuleFor(p => p.BackgroundImage)
           .NotNull().WithMessage("BackgroundImage can't be null")
           .Must(p => p.Length > 0).WithMessage("BackgroundImage can't be empty")
           .Must(p => p.ContentType == "image/png" || p.ContentType == "image/jpeg")
           .WithMessage("Image must be jpg/png")
           .Must(p => p.Length <= 15 * 1024 * 1024)
           .WithMessage("Image size must be lower than 15 mb");
    }
}
