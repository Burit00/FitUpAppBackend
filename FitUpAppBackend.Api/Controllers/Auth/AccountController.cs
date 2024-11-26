using FitUpAppBackend.Application.Identity.Commands.EmailVerification;
using FitUpAppBackend.Application.Identity.Commands.ResetPassword;
using FitUpAppBackend.Application.Identity.Commands.ResetPasswordRequest;
using FitUpAppBackend.Application.Identity.Commands.SignIn;
using FitUpAppBackend.Application.Identity.Commands.SignUp;
using FitUpAppBackend.Core.Identity.DTO;
using FitUpAppBackend.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Mvc;

namespace FitUpAppBackend.Api.Controllers.Auth;

public class AccountController : BaseApiController
{
    private readonly ICommandDispatcher _commandDispatcher;

    public AccountController(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [HttpPost("sign-up")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SignUp(SignUpCommand request, CancellationToken cancellationToken = default)
    {
        await _commandDispatcher.DispatchAsync(request, cancellationToken);
        return Ok();
    }

    [HttpPost("sign-in")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<JsonWebToken>> SignIn(SignInCommand request,
        CancellationToken cancellationToken = default)
    {
        var jwt = await _commandDispatcher.DispatchAsync<SignInCommand, JsonWebToken>(request, cancellationToken);
        return Ok(jwt);
    }

    [HttpPost("email-verification")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> EmailVerification(EmailVerificationCommand request,
        CancellationToken cancellationToken = default)
    {
        await _commandDispatcher.DispatchAsync(request, cancellationToken);
        return Ok();
    }

    [HttpPost("reset-password-request")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ResetPasswordRequest(ResetPasswordRequestCommand request,
        CancellationToken cancellationToken = default)
    {
        await _commandDispatcher.DispatchAsync(request, cancellationToken);
        return Ok();
    }

    [HttpPost("reset-password")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ResetPassword(ResetPasswordCommand request,
        CancellationToken cancellationToken = default)
    {
        await _commandDispatcher.DispatchAsync(request, cancellationToken);
        return Ok();
    }
}