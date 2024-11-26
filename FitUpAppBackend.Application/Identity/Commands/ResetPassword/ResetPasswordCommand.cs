using FitUpAppBackend.Shared.Abstractions.Commands;

namespace FitUpAppBackend.Application.Identity.Commands.ResetPassword;

public sealed record ResetPasswordCommand(string Email, string Token, string Password, string ConfirmPassword) : ICommand;