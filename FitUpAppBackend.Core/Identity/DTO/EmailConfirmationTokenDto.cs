namespace FitUpAppBackend.Core.Identity.DTO;

public sealed class EmailConfirmationTokenDto
{
    public string Token { get; set; }
    public Guid UserId { get; set; }
}