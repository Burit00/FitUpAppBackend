using FitUpAppBackend.Api.Attributes;
using FitUpAppBackend.Application.Common;
using FitUpAppBackend.Application.WorkoutSets.Commands.CreateWorkoutSet;
using FitUpAppBackend.Application.WorkoutSets.Commands.UpdateWorkoutSet;
using FitUpAppBackend.Core.Identity.Static;
using FitUpAppBackend.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Mvc;

namespace FitUpAppBackend.Api.Controllers.WorkoutSets;

public class WorkoutSetsController : BaseApiController
{
    private readonly ICommandDispatcher _commandDispatcher;

    public WorkoutSetsController(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [HttpPost]
    [ApiAuthorize(Roles = UserRoles.Admin)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Guid>> Create(CreateWorkoutSetCommand command, CancellationToken cancellationToken = default)
    {
        var result = await _commandDispatcher.DispatchAsync<CreateWorkoutSetCommand, CreateOrUpdateResponse>(command, cancellationToken);
        return Created("", result);
    }

    [HttpPut]
    [ApiAuthorize(Roles = UserRoles.Admin)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Guid>> Update(UpdateWorkoutSetCommand command, CancellationToken cancellationToken = default)
    {
        var result = await _commandDispatcher.DispatchAsync<UpdateWorkoutSetCommand, CreateOrUpdateResponse>(command, cancellationToken);
        return Ok(result.Id);
    }
}