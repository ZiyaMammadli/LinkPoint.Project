using FluentValidation;
using LinkPoint.Business.DTOs.ConversationDTOs;

namespace LinkPoint.Business.Validators.ConversationValidators;

public class ConversationGetValidator:AbstractValidator<ConversationGetDto>
{
    public ConversationGetValidator()
    {
        RuleFor(conv => conv.ConversationId)
        .NotNull().WithMessage("ConversationId can't be null")
        .NotEmpty().WithMessage("ConversationId can't be empty");
        RuleFor(conv => conv.User1Id)
        .NotNull().WithMessage("User1Id can't be null")
        .NotEmpty().WithMessage("User1Id can't be empty");
        RuleFor(conv => conv.User2Id)
        .NotNull().WithMessage("User2Id can't be null")
        .NotEmpty().WithMessage("User2Id can't be empty");

    }
}
