using FluentValidation;
using LinkPoint.Business.DTOs.PostDTOs;

namespace LinkPoint.Business.Validators.PostValidators;

public class PostUpdateWithTextValidator:AbstractValidator<PostUpdateWithTextDto>
{
    public PostUpdateWithTextValidator()
    {
        RuleFor(p => p.Id)
           .NotNull().WithMessage("Id can't be null")
           .NotEmpty().WithMessage("Id can't be empty");
        RuleFor(p => p.Text)
           .NotNull().WithMessage("Text can't be null")
           .NotEmpty().WithMessage("Text can't be empty")
           .MaximumLength(300).WithMessage("Max lenth of text is 300 ");
    }
}
