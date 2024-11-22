using FluentValidation;

namespace FitUpAppBackend.Application.Identity.Commands.ResetPassword;

public class ResetPasswordValidator : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordValidator()
    {
        RuleFor(resetPassword => resetPassword.Password)
            .Equal(resetPassword => resetPassword.ConfirmPassword);
    }
}