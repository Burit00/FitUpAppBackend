using FitUpAppBackend.Core.Identity.Services;
using FitUpAppBackend.Core.Integrations.Email.Services;
using FitUpAppBackend.Core.Integrations.Frontend.Configurations;
using FitUpAppBackend.Shared.Abstractions.Commands;

namespace FitUpAppBackend.Application.Identity.Commands.SignUp;

public sealed class SignUpHandler : ICommandHandler<SignUpCommand>
{
    private readonly IIdentityService _identityService;
    private readonly IEmailService _emailService;
    private readonly FrontendConfig _frontendConfig;

    private readonly string _emailSubject = "FitUP: Email Verification";
    private readonly string _emailVerificationTemplateId = "d-55cc0d0117a34bc68a926fb77cd73d90";

    public SignUpHandler(IIdentityService identityService, IEmailService emailService, FrontendConfig frontendConfig)
    {
        _identityService = identityService;
        _emailService = emailService;
        _frontendConfig = frontendConfig;
    }

    public async Task HandleAsync(SignUpCommand request, CancellationToken cancellationToken)
    {
        await _identityService.SignUpAsync(request.Email, request.Password, cancellationToken);
        var emailVerificationToken =
            await _identityService.GenerateEmailConfirmationTokenAsync(request.Email, cancellationToken);
        
        var email = request.Email;
        var token = emailVerificationToken;

        var templateData = new
        {
            url =
                $"{_frontendConfig.BaseUrl}/email-confirmation?{nameof(token)}={token}&{nameof(email)}={email}",
        };
        
       await _emailService.SendTemplateMailAsync(request.Email, _emailSubject, _emailVerificationTemplateId, templateData);
    }
}