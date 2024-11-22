using FitUpAppBackend.Core.Identity.Services;
using FitUpAppBackend.Shared.Abstractions.Commands;

namespace FitUpAppBackend.Application.Identity.Commands.EmailVerification;

public sealed class EmailVerificationHandler : ICommandHandler<EmailVerificationCommand>
{
    private readonly IIdentityService _identityService;

    public EmailVerificationHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task HandleAsync(EmailVerificationCommand command, CancellationToken cancellationToken)
    {
        await _identityService.ConfirmEmailAsync(command.UserId, command.Token, cancellationToken);
    }
}