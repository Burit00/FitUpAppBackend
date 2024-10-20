namespace FitUpAppBackend.Core.Identity.DTO;

public class JsonWebToken
{
    public string AccessToken { get; init; }
    public long Expires { get; init; }
    
    public Guid UserId { get; init; }
    public string Email { get; init; }
    public ICollection<string> Roles { get; init; }
    public IDictionary<string, string> Claims { get; init; }
}