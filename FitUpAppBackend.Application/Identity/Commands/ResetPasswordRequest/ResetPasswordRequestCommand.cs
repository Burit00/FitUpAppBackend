using FitUpAppBackend.Shared.Abstractions.Commands;

namespace FitUpAppBackend.Application.Identity.Commands.ResetPasswordRequest;

public sealed record ResetPasswordRequestCommand(string Email) : ICommand
{
    
}