using FitUpAppBackend.Shared.Abstractions.Commands;

namespace FitUpAppBackend.Application.Identity.Commands.EmailVerification;

public sealed record EmailVerificationCommand(Guid UserId, string Token) : ICommand;