using FluentValidation;
using Kiwi.WebApi.Models.Content;
using Kiwi.WebApi.Models;

namespace Kiwi.WebApi.Validators;

public sealed class CreateContentRequestValidator : AbstractValidator<CreateContentRequest>
{
    public CreateContentRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(200).WithMessage("Description must not exceed 100 characters.");
    }
}