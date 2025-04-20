namespace Kiwi.Application.Content.Commands.CreateContent;

public class CreateContentCommandValidator : AbstractValidator<CreateContentCommand>
{
    public CreateContentCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(200).WithMessage("Description must not exceed 100 characters.");

        RuleFor(x => x.ContentType)
            .NotNull().WithMessage("ContentType is required.");
    }
}