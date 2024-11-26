using FitUpAppBackend.Core.Identity.Entities;
using FitUpAppBackend.Core.Identity.Services;
using FitUpAppBackend.Core.Integrations.Email.Services;
using FitUpAppBackend.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Identity;

namespace FitUpAppBackend.Application.Identity.Commands.ResetPasswordRequest;

public sealed class ResetPasswordRequestHandler : ICommandHandler<ResetPasswordRequestCommand>
{
    private readonly IIdentityService _identityService;
    private readonly IEmailService _emailService;

    private readonly string _resetPasswordTemplateId = "d-2e01bae7bcf7497a821c74528552c406";
    private readonly string _resetPasswordSubject = "Fit UP change password request";

    public ResetPasswordRequestHandler(IIdentityService identityService, UserManager<User> userManager,
        IEmailService emailService)
    {
        _identityService = identityService;
        _emailService = emailService;
    }

    public async Task HandleAsync(ResetPasswordRequestCommand command, CancellationToken cancellationToken)
    {
        var passwordResetToken = await _identityService.GeneratePasswordResetTokenAsync(command.Email, cancellationToken);

        var email = command.Email;
        var token = passwordResetToken;
        
        var linkToResetPassword = $"http://localhost:3000/reset-password?{nameof(token)}={token}&{nameof(email)}={email}";

        var templateData = new { url = linkToResetPassword };

        await _emailService.SendTemplateMailAsync(command.Email, _resetPasswordSubject, _resetPasswordTemplateId,
            templateData);
    }
}