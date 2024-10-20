using System.Security.Claims;
using FitUpAppBackend.Core.Common.Services;
using Microsoft.AspNetCore.Http;

namespace FitUpAppBackend.Infrastructure.Common.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid UserId => GetClaimAsGuid(ClaimTypes.NameIdentifier);

    private Guid GetClaimAsGuid(string claimType)
    {
        var guidAsString = _httpContextAccessor.HttpContext?.User.FindFirstValue(claimType);
        
        return string.IsNullOrEmpty(guidAsString) ? Guid.Empty : Guid.Parse(guidAsString);
    }
}