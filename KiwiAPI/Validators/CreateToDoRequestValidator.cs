using FluentValidation;
using KiwiAPI.Models.ToDo;

namespace KiwiAPI.Validators;

public class CreateToDoRequestValidator : AbstractValidator<CreateToDoRequest>
{
    public CreateToDoRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(200).WithMessage("Description must not exceed 100 characters.");
        
        RuleFor(x => x.ContentId)
            .NotEmpty().WithMessage("Content Id is required.")
            .GreaterThan(0).WithMessage("Content Id must be greater than zero.");
    }
}