using FluentValidation;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.BackgroundImageDTOs;

namespace LinkPoint.Business.Validators.DefaulImagesValidators;

public class BackgroundImageDeleteValidator:AbstractValidator<BackgroundImageDeleteDto>
{
    public BackgroundImageDeleteValidator()
    {
        RuleFor(p => p.ImageId)
            .NotNull().WithMessage("ImageId can't be null")
            .NotEmpty().WithMessage("ImageId can't be empty");
    }
}
