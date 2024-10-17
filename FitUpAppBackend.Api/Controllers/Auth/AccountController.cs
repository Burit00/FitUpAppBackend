using FitUpAppBackend.Application.Identity.Commands.SignUp;
using FitUpAppBackend.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Mvc;

namespace FitUpAppBackend.Api.Controllers.Auth;

public class AccountController: BaseApiController
{
    private readonly ICommandDispatcher _commandDispatcher;

    public AccountController(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }
    
    [HttpPost("sign-up")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> SignUp(SignUpCommand request, CancellationToken cancellationToken = default)
    {
        await _commandDispatcher.DispatchAsync(request, cancellationToken);
        return CreatedAtAction(nameof(SignUp), new { id = request.Email }, null);
    }
}