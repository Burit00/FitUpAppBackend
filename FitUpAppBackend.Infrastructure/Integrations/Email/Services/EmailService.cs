using FitUpAppBackend.Core.Integrations.Email.Configurations;
using FitUpAppBackend.Core.Integrations.Email.Services;
using MailKit.Net.Smtp;
using MimeKit;

namespace FitUpAppBackend.Infrastructure.Integrations.Email.Services;

public class EmailService : IEmailService
{
    private readonly EmailConfig _emailConfig;

    public EmailService(EmailConfig emailConfig)
    {
        _emailConfig = emailConfig;
    }
    public async Task SendAsync(string email, string subject, string body)
    {
        MimeMessage emailMessage = CreateMessage(email, subject, body);
        
        using var smtpClient = new SmtpClient();
        smtpClient.Timeout = 20;
        await smtpClient.ConnectAsync(_emailConfig.Server, _emailConfig.Port, true);
        await smtpClient.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);
        await smtpClient.SendAsync(emailMessage);
        await smtpClient.DisconnectAsync(true);
    }

    private MimeMessage CreateMessage(string email, string subject, string body)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(_emailConfig.SenderName, _emailConfig.SenderEmail));
        message.To.Add(new MailboxAddress(email, email));
        message.Subject = subject;

        var bodyBuilder = new BodyBuilder();
        bodyBuilder.TextBody = body;
        message.Body = bodyBuilder.ToMessageBody();        
        
        return message;
    }
}