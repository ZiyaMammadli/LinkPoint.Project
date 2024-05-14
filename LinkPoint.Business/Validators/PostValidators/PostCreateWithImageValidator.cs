using FluentValidation;
using LinkPoint.Business.DTOs.PostDTOs;

namespace LinkPoint.Business.Validators.PostValidators;

public class PostCreateWithImageValidator:AbstractValidator<PostCreateWithImageDto>
{
    public PostCreateWithImageValidator()
    {
        RuleFor(p => p.Text)
           .MaximumLength(300).WithMessage("Max lenth of text is 300 ");
        RuleFor(p => p.PostImageFile)
            .NotNull()
            .Must(p => p.ContentType == "image/png" || p.ContentType == "image/jpeg")
            .WithMessage("Image must be jpg/png")
            .Must(p => p.Length <= 15 * 1024 * 1024)
            .WithMessage("Image size must be lower than 15 mb");
    }
}
