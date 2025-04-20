namespace Kiwi.Application.User.Commands.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotNull().WithMessage("Id is required");
        
        RuleFor(x => x.Name)
            .MinimumLength(2).WithMessage("Name must be at least 2 characters long.")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");
        
        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Valid email is required.");
    }
}