using FluentValidation;

namespace FitUpAppBackend.Application.Identity.Commands.SignIn;

public class SignInValidator : AbstractValidator<SignInCommand>
{
    public SignInValidator()
    {
        RuleFor(command => command.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email is invalid");

        RuleFor(command => command.Password)
            .NotEmpty().WithMessage("Password is required");
    }
}