using FluentValidation;
using LinkPoint.Business.DTOs.CommentDTOs;

namespace LinkPoint.Business.Validators.CommentValidators;

public class CommentDeleteValidator:AbstractValidator<CommentDeleteDto>
{
    public CommentDeleteValidator()
    {
        RuleFor(c => c.CommentId)
           .NotNull().WithMessage("CommentId can't be null")
           .NotEmpty().WithMessage("CommentId can't be empty");
    }
}
