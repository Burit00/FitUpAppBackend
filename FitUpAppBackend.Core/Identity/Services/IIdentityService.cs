using FitUpAppBackend.Core.Identity.DTO;

namespace FitUpAppBackend.Core.Identity.Services;

public interface IIdentityService
{
    public Task SignUpAsync(string email, string password, CancellationToken cancellationToken);
    public Task<JsonWebToken> SignInAsync(string email, string password, CancellationToken cancellationToken);
}