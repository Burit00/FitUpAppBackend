using FitUpAppBackend.Api.Attributes;
using FitUpAppBackend.Application.SetParameters.Commands.UpdateSetParameter;
using FitUpAppBackend.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Mvc;

namespace FitUpAppBackend.Api.Controllers.SetParameters;

[ApiAuthorize(Roles = "User")]
public class SetParametersController : BaseApiController
{
    private readonly ICommandDispatcher _commandDispatcher;

    public SetParametersController(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [HttpPut]
    public async Task<IActionResult> UpdateSetParametersValue([FromBody] UpdateSetParameterRangeCommand command,
        CancellationToken cancellationToken)
    {
        await _commandDispatcher.DispatchAsync(command, cancellationToken);
        return Ok();
    }
}