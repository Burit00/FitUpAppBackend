using FitUpAppBackend.Core.Identity.Services;
using FitUpAppBackend.Shared.Abstractions.Commands;

namespace FitUpAppBackend.Application.Identity.Commands.ResetPassword;

public sealed class ResetPasswordHandler : ICommandHandler<ResetPasswordCommand>
{
    private readonly IIdentityService _identityService;

    public ResetPasswordHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }
    
    public async Task HandleAsync(ResetPasswordCommand command, CancellationToken cancellationToken)
    {
        
        await _identityService.ResetPasswordAsync(command.UserId, command.Password, command.Token, cancellationToken);
    }
}