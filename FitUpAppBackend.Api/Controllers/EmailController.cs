using FitUpAppBackend.Application.Email;
using FitUpAppBackend.Core.Integrations.Email.Services;
using Microsoft.AspNetCore.Mvc;

namespace FitUpAppBackend.Api.Controllers;

public class EmailController : BaseApiController
{
    private readonly IEmailService _emailService;

    public EmailController(IEmailService emailService)
    {
        _emailService = emailService;
    }
    [HttpPost]
    public async Task<ActionResult<bool>> SendEmail([FromBody] EmailCommand emailCommand)
    {
        await _emailService.SendAsync(emailCommand.Email, emailCommand.Subject, emailCommand.Body);
        return Ok(true);
    }
}