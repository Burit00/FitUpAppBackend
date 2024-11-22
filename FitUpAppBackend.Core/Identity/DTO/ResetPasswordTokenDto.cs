namespace FitUpAppBackend.Core.Identity.DTO;

public sealed class ResetPasswordTokenDto
{
    public string Token { get; set; }
    public Guid UserId { get; set; }
}