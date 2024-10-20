using FitUpAppBackend.Core.Identity.Services;
using FitUpAppBackend.Shared.Abstractions.Commands;

namespace FitUpAppBackend.Application.Identity.Commands.SignUp;

public class SignUpHandler: ICommandHandler<SignUpCommand>
{
    private readonly IIdentityService _identityService;

    public SignUpHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }
    
    public async Task HandleAsync(SignUpCommand request, CancellationToken cancellationToken)
    {
        await _identityService.SignUpAsync(request.Email, request.Password, cancellationToken);
    }
    
}