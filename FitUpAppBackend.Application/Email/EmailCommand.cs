using FitUpAppBackend.Shared.Abstractions.Commands;

namespace FitUpAppBackend.Application.Email;

public sealed record EmailCommand(string Email, string Subject, string Body) : ICommand;