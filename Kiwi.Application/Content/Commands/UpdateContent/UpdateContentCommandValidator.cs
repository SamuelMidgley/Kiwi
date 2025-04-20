namespace Kiwi.Application.Content.Commands.UpdateContent;

public class UpdateContentCommandValidator : AbstractValidator<UpdateContentCommand>
{
    public UpdateContentCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotNull().WithMessage("Id cannot be null.");
        
        RuleFor(x => x.Title)
            .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(200).WithMessage("Description must not exceed 100 characters.");
    }
}