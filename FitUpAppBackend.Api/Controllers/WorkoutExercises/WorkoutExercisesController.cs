using FitUpAppBackend.Api.Attributes;
using FitUpAppBackend.Application.Common;
using FitUpAppBackend.Application.WorkoutExercises.Commands.CreateWorkoutExercise;
using FitUpAppBackend.Application.WorkoutExercises.Commands.DeleteWorkoutExercise;
using FitUpAppBackend.Application.WorkoutExercises.Commands.UpdateWorkoutExercise;
using FitUpAppBackend.Application.WorkoutExercises.DTO;
using FitUpAppBackend.Application.WorkoutExercises.Queries.GetWorkoutExercise;
using FitUpAppBackend.Core.Identity.Static;
using FitUpAppBackend.Shared.Abstractions.Commands;
using FitUpAppBackend.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace FitUpAppBackend.Api.Controllers.WorkoutExercises;

[ApiAuthorize(Roles = UserRoles.User)]
public class WorkoutExercisesController : BaseApiController
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public WorkoutExercisesController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreateOrUpdateResponse>> Create([FromBody] CreateWorkoutExerciseCommand command, CancellationToken cancellationToken = default)
    {
        var result = await _commandDispatcher.DispatchAsync<CreateWorkoutExerciseCommand, CreateOrUpdateResponse>(command, cancellationToken);
        return CreatedAtAction(nameof(Get), new { workoutExerciseId = result.Id }, result);
    }

    [HttpPut("{workoutExerciseId:guid}/change-order")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreateOrUpdateResponse>> Update([FromRoute] Guid workoutExerciseId, [FromBody] UpdateWorkoutExerciseCommand command, CancellationToken cancellationToken = default)
    {
        command.WorkoutExerciseMovedId = workoutExerciseId;
        await _commandDispatcher.DispatchAsync(command, cancellationToken);
        return Ok();
    }

    [HttpGet("{workoutExerciseId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WorkoutExerciseDetailsDto>> Get([FromRoute] Guid workoutExerciseId, CancellationToken cancellationToken = default)
    {
        var result = await _queryDispatcher.DispatchAsync<GetWorkoutExerciseQuery, WorkoutExerciseDetailsDto>(new GetWorkoutExerciseQuery(workoutExerciseId), cancellationToken);
        return Ok(result);
    }

    [HttpDelete("{workoutExerciseId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] Guid workoutExerciseId, CancellationToken cancellationToken = default)
    {
        await _commandDispatcher.DispatchAsync(new DeleteWorkoutExerciseCommand(workoutExerciseId), cancellationToken);
        return Ok();
    }
}