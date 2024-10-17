using FluentValidation;

namespace FitUpAppBackend.Application.Identity.Commands.SignUp;

public sealed class SignUpValidator : AbstractValidator<SignUpCommand>
{
    public SignUpValidator()
    {
        Console.WriteLine("***\n***\n***\n***\nValidation***\n***\n***\n***\n");
        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("Invalid email address.");

        RuleFor(x => x.Password)
            .Equal(x => x.ConfirmPassword)
            .WithMessage("Passwords do not match.");
    }
}