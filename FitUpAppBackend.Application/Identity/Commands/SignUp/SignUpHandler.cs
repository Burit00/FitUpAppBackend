using FitUpAppBackend.Core.Identity.Services;
using FitUpAppBackend.Core.Integrations.Email.Services;
using FitUpAppBackend.Shared.Abstractions.Commands;

namespace FitUpAppBackend.Application.Identity.Commands.SignUp;

public sealed class SignUpHandler : ICommandHandler<SignUpCommand>
{
    private readonly IIdentityService _identityService;
    private readonly IEmailService _emailService;
    
    private readonly string _emailSubject = "FitUP: Email Verification";
    private readonly string _emailVerificationTemplateId = "d-55cc0d0117a34bc68a926fb77cd73d90";

    public SignUpHandler(IIdentityService identityService, IEmailService emailService)
    {
        _identityService = identityService;
        _emailService = emailService;
    }

    public async Task HandleAsync(SignUpCommand request, CancellationToken cancellationToken)
    {
        await _identityService.SignUpAsync(request.Email, request.Password, cancellationToken);
        var emailVerificationToken =
            await _identityService.GenerateEmailConfirmationTokenAsync(request.Email, cancellationToken);

        var templateData = new
        {
            url =
                $"http://localhost:3000/email-verification?token={emailVerificationToken.Token}&userId={emailVerificationToken.UserId}",
        };
        
       await _emailService.SendTemplateMailAsync(request.Email, _emailSubject, _emailVerificationTemplateId, templateData);
    }
}