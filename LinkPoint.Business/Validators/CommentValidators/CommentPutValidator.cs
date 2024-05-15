using FluentValidation;
using LinkPoint.Business.DTOs.CommentDTOs;

namespace LinkPoint.Business.Validators.CommentValidators;

public class CommentPutValidator:AbstractValidator<CommentPutDto>
{
    public CommentPutValidator()
    {
        RuleFor(c => c.CommentId)
            .NotNull().WithMessage("CommentId can't be null")
            .NotEmpty().WithMessage("CommentId can't be empty");
        RuleFor(c => c.Text)
           .NotNull().WithMessage("Text can't be null")
           .NotEmpty().WithMessage("Text can't be empty")
           .MaximumLength(300).WithMessage("Max lenth of text is 300 ");
    }
}
