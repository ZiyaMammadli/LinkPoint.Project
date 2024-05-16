using FluentValidation;
using LinkPoint.Business.DTOs.PostDTOs;

namespace LinkPoint.Business.Validators.PostValidators;

public class PostCreateWithVideoValidator:AbstractValidator<PostCreateWithVideoDto>
{
    public PostCreateWithVideoValidator()
    {
        RuleFor(p => p.Text)
           .MaximumLength(300).WithMessage("Max lenth of text is 300 ");
        RuleFor(p => p.PostVideoFile)
            .NotNull().WithMessage("PostImage can't be null")
            .Must(p => p.Length > 0).WithMessage("PostImage can't be empty")
            .Must(p => p.ContentType == "video/mp4" || p.ContentType == "video/webm")
            .WithMessage("Video must be mp4/webm")
            .Must(p => p.Length <= 100 * 1024 * 1024)
            .WithMessage("Video size must be lower than 100 mb");
    }
}
