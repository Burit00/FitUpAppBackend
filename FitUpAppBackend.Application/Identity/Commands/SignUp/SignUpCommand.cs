using FitUpAppBackend.Shared.Abstractions.Commands;

namespace FitUpAppBackend.Application.Identity.Commands.SignUp;

public sealed record SignUpCommand(string Email, string Password, string ConfirmPassword) : ICommand;