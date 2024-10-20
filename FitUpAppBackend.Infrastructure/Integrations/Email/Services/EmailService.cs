using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using FitUpAppBackend.Core.Integrations.Email.Configurations;
using FitUpAppBackend.Core.Integrations.Email.Services;
using MailKit.Net.Smtp;
using MimeKit;

namespace FitUpAppBackend.Infrastructure.Integrations.Email.Services;

public class EmailService : IEmailService
{
    private sealed record MailtrapFrom(string Email, string Name);
    private sealed record MailtrapTo(string Email);
    private class MailtrapBody
    {
        public MailtrapFrom From { get; set; }
        public IEnumerable<MailtrapTo> To { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
        public string Category { get; set; }
    }
    
    private readonly EmailConfig _emailConfig;

    public EmailService(EmailConfig emailConfig)
    {
        _emailConfig = emailConfig;
    }

    public async Task SendAsync(string email, string subject, string body)
    {
        MimeMessage emailMessage = CreateMessage(email, subject, body);

        using (var smtpClient = new SmtpClient())
        {
            smtpClient.Timeout = 30000;
            await smtpClient.ConnectAsync(_emailConfig.Server, _emailConfig.Port, false);
            await smtpClient.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);
            await smtpClient.SendAsync(emailMessage);
            await smtpClient.DisconnectAsync(true);
        }
    }

    public async Task SendByRequestAsync(string email, string subject, string body)
    {
        var requestBody = new MailtrapBody
        {
            From = new MailtrapFrom("hello@demomailtrap.com", _emailConfig.SenderName),
            To = new List<MailtrapTo> { new MailtrapTo(email) },
            Subject = subject,
            Category = "Test",
            Text = body
        };

        var requestBodyJson = JsonSerializer.Serialize(requestBody);
            // $"\"{{\\\"from\\\":{{\\\"email\\\":\\\"{_emailConfig.SenderEmail}\\\",\\\"name\\\":\\\"{_emailConfig.SenderName}\\\"}},\\\"to\\\":[{{\\\"email\\\":\\\"{email}\\\"}}],\\\"subject\\\":\\\"{subject}\\\",\\\"text\\\":\\\"{body}\\\",\\\"category\\\":\\\"Test\\\"}}\"";
            // $"\"{{\\\"from\\\":{{\\\"email\\\":\\\"{_emailConfig.SenderEmail}\\\",\\\"name\\\":\\\"{_emailConfig.SenderName}\\\"}},\\\"to\\\":[{{\\\"email\\\":\\\"pmorawczynski01@gmail.com\\\"}}],\\\"subject\\\":\\\"subject\\\",\\\"text\\\":\\\"body\\\",\\\"category\\\":\\\"Test\\\"}}\"";
        var client = new HttpClient();

        var request = new HttpRequestMessage(HttpMethod.Post, "https://send.api.mailtrap.io/api/send");
        client.DefaultRequestHeaders
            .Accept
            .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        request.Headers.Add("Authorization", "Bearer d0d462315d44d9a681586f41411da7a0");
        // request.Headers.Add("Content-Type", "application/json");
        request.Content = new StringContent(requestBodyJson, Encoding.UTF8, "application/json");
        var response = await client.SendAsync(request);
        Console.WriteLine(response.Content.ReadAsStringAsync().Result);
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