namespace FitUpAppBackend.Core.Identity.Configuration;

public sealed class AuthConfig
{
    public string JwtKey { get; init; }
    public string JwtIssuer { get; init; }
    public TimeSpan Expires { get; init; }
}