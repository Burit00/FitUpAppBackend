namespace FitUpAppBackend.Infrastructure.Integrations.Email.Configurations;

public class SendGridConfig
{
    public string ApiKey { get; set; } 
    public string SenderEmail { get; set; } 
    public string SenderName { get; set; } 
}