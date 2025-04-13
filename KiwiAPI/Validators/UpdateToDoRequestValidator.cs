using FluentValidation;
using KiwiAPI.Models.ToDo;

namespace KiwiAPI.Validators;

public class UpdateToDoRequestValidator : AbstractValidator<UpdateToDoRequest>
{
    public UpdateToDoRequestValidator()
    {
        RuleFor(x => x.Title)
            .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(200).WithMessage("Description must not exceed 100 characters.");
    }
}