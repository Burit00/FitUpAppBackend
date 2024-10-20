using System.Security.Claims;
using FitUpAppBackend.Core.Identity.DTO;

namespace FitUpAppBackend.Core.Identity.Services;

public interface ITokenService
{
    public Task<JsonWebToken> GenerateJwtAsync(Guid userId, string email, ICollection<string> roles, ICollection<Claim> claims, CancellationToken cancellationToken);
}