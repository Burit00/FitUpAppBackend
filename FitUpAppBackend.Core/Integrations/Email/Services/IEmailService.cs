namespace FitUpAppBackend.Core.Integrations.Email.Services;

public interface IEmailService
{
    public Task SendAsync(string email, string subject, string body);
}