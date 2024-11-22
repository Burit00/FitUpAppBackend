using FitUpAppBackend.Core.Identity.DTO;
using FitUpAppBackend.Core.Identity.Entities;

namespace FitUpAppBackend.Core.Identity.Services;

public interface IIdentityService
{
    public Task SignUpAsync(string email, string password, CancellationToken cancellationToken);
    public Task<JsonWebToken> SignInAsync(string email, string password, CancellationToken cancellationToken);
    public Task<EmailConfirmationTokenDto> GenerateEmailConfirmationTokenAsync(string email, CancellationToken cancellationToken);
    public Task ConfirmEmailAsync(Guid userId, string token, CancellationToken cancellationToken);
    public Task<ResetPasswordTokenDto> GeneratePasswordResetTokenAsync(string email, CancellationToken cancellationToken);
    public Task ResetPasswordAsync(Guid userId, string password, string resetPasswordToken, CancellationToken cancellationToken);
    public Task<User> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken);
}