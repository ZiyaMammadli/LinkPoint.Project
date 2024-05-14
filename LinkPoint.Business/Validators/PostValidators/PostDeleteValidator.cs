using FluentValidation;
using LinkPoint.Business.DTOs.PostDTOs;

namespace LinkPoint.Business.Validators.PostValidators;

public class PostDeleteValidator:AbstractValidator<PostDeleteDto>
{
    public PostDeleteValidator()
    {
        RuleFor(p => p.Id)
        .NotNull().WithMessage("Id can't be null")
        .NotEmpty().WithMessage("Id can't be empty");
    }
}
