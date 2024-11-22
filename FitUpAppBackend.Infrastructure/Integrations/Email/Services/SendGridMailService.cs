using FitUpAppBackend.Core.Integrations.Email.Services;
using FitUpAppBackend.Infrastructure.Integrations.Email.Configurations;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace FitUpAppBackend.Infrastructure.Integrations.Email.Services;

public class SendGridMailService : IEmailService
{
    private readonly SendGridConfig _config;


    public SendGridMailService(SendGridConfig config)
    {
        _config = config;
    }
    public async Task SendMailAsync(string email, string subject, string body)
    {
        
        var client = new SendGridClient(_config.ApiKey);
        var from = new EmailAddress(_config.SenderEmail, _config.SenderName);
        var to = new EmailAddress(email);
        
        var plainTextContent = body;
        var htmlContent = body;
        
        var message = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        var response = await client.SendEmailAsync(message);
    }

    public async Task SendTemplateMailAsync(string email, string subject, string templateId, object templateData)
    {
        var client = new SendGridClient(_config.ApiKey);
        var from = new EmailAddress(_config.SenderEmail, _config.SenderName);
        var to = new EmailAddress(email);
        
        var message = MailHelper.CreateSingleTemplateEmail(from, to, templateId, templateData);
        await client.SendEmailAsync(message);
    }
}