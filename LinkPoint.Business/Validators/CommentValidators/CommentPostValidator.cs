using FluentValidation;
using LinkPoint.Business.DTOs.CommentDTOs;

namespace LinkPoint.Business.Validators.CommentValidators;

public class CommentPostValidator:AbstractValidator<CommentPostDto>
{
    public CommentPostValidator()
    {
        RuleFor(c=>c.PostId)
            .NotNull().WithMessage("PostId can't be null")
            .NotEmpty().WithMessage("PostId can't be empty");
        RuleFor(c => c.Text)
           .NotNull().WithMessage("Text can't be null")
           .NotEmpty().WithMessage("Text can't be empty")
           .MaximumLength(300).WithMessage("Max lenth of text is 300 ");
    }
}
