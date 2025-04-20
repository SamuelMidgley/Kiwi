namespace Kiwi.Application.Auth.Commands.LoginUser;

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotNull().WithMessage("Email is required")
            .EmailAddress().WithMessage("A valid email address is required");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required");
    }
}