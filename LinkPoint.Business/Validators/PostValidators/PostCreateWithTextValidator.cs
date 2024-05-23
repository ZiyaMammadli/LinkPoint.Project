using FluentValidation;
using LinkPoint.Business.DTOs.PostDTOs;

namespace LinkPoint.Business.Validators.PostValidators;

public class PostCreateWithTextValidator:AbstractValidator<PostCreateWithTextDto>
{
    public PostCreateWithTextValidator()
    {
        RuleFor(c => c.UserId)
            .NotNull().WithMessage("PostId can't be null")
            .NotEmpty().WithMessage("PostId can't be empty");
        RuleFor(p => p.Text)
           .NotNull().WithMessage("Text can't be null")
           .NotEmpty().WithMessage("Text can't be empty")
           .MaximumLength(300).WithMessage("Max lenth of text is 300 ");
    }
}
