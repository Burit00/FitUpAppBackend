using FitUpAppBackend.Api.Attributes;
using FitUpAppBackend.Application.Workouts.Commands.CreateWorkout;
using FitUpAppBackend.Application.Workouts.DTO;
using FitUpAppBackend.Application.Workouts.Queries;
using FitUpAppBackend.Core.Identity.Static;
using FitUpAppBackend.Shared.Abstractions.Commands;
using FitUpAppBackend.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace FitUpAppBackend.Api.Controllers.Workouts;

public class WorkoutsController : BaseApiController
{
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly ICommandDispatcher _commandDispatcher;

    public WorkoutsController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
    {
        _queryDispatcher = queryDispatcher;
        _commandDispatcher = commandDispatcher;
    }

    [HttpGet]
    [ApiAuthorize(Roles = UserRoles.User)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<BrowseWorkoutsDto>>> GetWorkoutDates([FromQuery] BrowseWorkoutsQuery query, CancellationToken cancellationToken = default)
    {
        var result = await _queryDispatcher.DispatchAsync<BrowseWorkoutsQuery, IEnumerable<BrowseWorkoutsDto>>(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{workoutId:Guid}")]
    [ApiAuthorize(Roles = UserRoles.User)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WorkoutDto>> GetWorkoutDates([FromRoute] Guid workoutId, CancellationToken cancellationToken = default)
    {
        var result = await _queryDispatcher.DispatchAsync<GetWorkoutQuery, WorkoutDto>(new GetWorkoutQuery(workoutId), cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    [ApiAuthorize(Roles = UserRoles.User)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Guid>> CreateWorkout([FromBody] CreateWorkoutCommand request, CancellationToken cancellationToken = default)
    {
        var result = await _commandDispatcher.DispatchAsync<CreateWorkoutCommand, Guid>(request, cancellationToken);
        return Created("xd", result);
    }
}