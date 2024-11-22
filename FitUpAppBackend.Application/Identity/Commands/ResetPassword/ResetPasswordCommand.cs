using FitUpAppBackend.Shared.Abstractions.Commands;

namespace FitUpAppBackend.Application.Identity.Commands.ResetPassword;

public sealed record ResetPasswordCommand(Guid UserId, string Token, string Password, string ConfirmPassword) : ICommand;