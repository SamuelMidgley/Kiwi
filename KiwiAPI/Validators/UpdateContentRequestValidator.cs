using FluentValidation;
using KiwiAPI.Models;
using KiwiAPI.Models.Content;

namespace KiwiAPI.Validators;

public sealed class UpdateContentRequestValidator : AbstractValidator<UpdateContentRequest>
{
    public UpdateContentRequestValidator()
    {
        RuleFor(x => x.Title)
            .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(200).WithMessage("Description must not exceed 100 characters.");
    }
}