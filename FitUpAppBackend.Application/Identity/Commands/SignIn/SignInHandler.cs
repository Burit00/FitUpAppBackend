using FitUpAppBackend.Core.Identity.DTO;
using FitUpAppBackend.Core.Identity.Services;
using FitUpAppBackend.Shared.Abstractions.Commands;

namespace FitUpAppBackend.Application.Identity.Commands.SignIn;

public class SignInHandler : ICommandHandler<SignInCommand, JsonWebToken>
{
    private readonly IIdentityService _identityService;

    public SignInHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<JsonWebToken> HandleAsync(SignInCommand request, CancellationToken cancellationToken)
    {
        var token = await _identityService.SignInAsync(request.Email, request.Password, cancellationToken);
        return token;
    }
}