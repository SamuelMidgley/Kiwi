namespace Kiwi.Application.Content.Commands.DeleteContent;

public class DeleteContentCommandValidator : AbstractValidator<DeleteContentCommand>
{
    public DeleteContentCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotNull().WithMessage("Id cannot be null.");
    }
}