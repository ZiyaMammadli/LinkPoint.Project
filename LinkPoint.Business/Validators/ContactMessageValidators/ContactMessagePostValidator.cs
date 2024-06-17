using FluentValidation;
using LinkPoint.Business.DTOs.ContactMessageDTOs;

namespace LinkPoint.Business.Validators.ContactMessageValidators;

public class ContactMessagePostValidator:AbstractValidator<ContactMessagePostDto>
{
    public ContactMessagePostValidator()
    {
        RuleFor(cm=>cm.UserId)
            .NotNull().WithMessage("UserId can't be null")
            .NotEmpty().WithMessage("UserId can't be empty");
        RuleFor(cm => cm.Name)
            .NotNull().WithMessage("Name can't be null")
            .NotEmpty().WithMessage("Name can't be empty")
            .MaximumLength(50).WithMessage("Max Lentgh of Name is 50");
        RuleFor(cm => cm.Email)
            .NotNull().WithMessage("Email can't be null")
            .NotEmpty().WithMessage("Email can't be empty")
            .MaximumLength(50).WithMessage("Max Lentgh of Email is 50");
        RuleFor(cm => cm.PhoneNumber)
            .NotNull().WithMessage("PhoneNumber can't be null")
            .NotEmpty().WithMessage("PhoneNumber can't be empty");
        RuleFor(cm => cm.Message)
            .NotNull().WithMessage("Message can't be null")
            .NotEmpty().WithMessage("Message can't be empty")
            .MaximumLength(350).WithMessage("Max Lentgh of Message is 350");
    }
}
