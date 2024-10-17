namespace FitUpAppBackend.Core.Identity;

public interface IIdentityService
{
    public Task SignUpAsync(string email, string password, CancellationToken cancellationToken);
}