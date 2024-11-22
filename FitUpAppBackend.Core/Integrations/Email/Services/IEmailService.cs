namespace FitUpAppBackend.Core.Integrations.Email.Services;

public interface IEmailService
{
    public Task SendMailAsync(string email, string subject, string body);
    public Task SendTemplateMailAsync(string email, string subject, string templateId, object templateData);
}