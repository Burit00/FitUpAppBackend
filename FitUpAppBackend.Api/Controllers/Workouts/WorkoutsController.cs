using FitUpAppBackend.Api.Attributes;
using FitUpAppBackend.Application.Common;
using FitUpAppBackend.Application.Workouts.Commands.CreateWorkout;
using FitUpAppBackend.Application.Workouts.DTO;
using FitUpAppBackend.Application.Workouts.Queries;
using FitUpAppBackend.Core.Identity.Static;
using FitUpAppBackend.Shared.Abstractions.Commands;
using FitUpAppBackend.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace FitUpAppBackend.Api.Controllers.Workouts;

[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public class WorkoutsController : BaseApiController
{
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly ICommandDispatcher _commandDispatcher;

    public WorkoutsController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
    {
        _queryDispatcher = queryDispatcher;
        _commandDispatcher = commandDispatcher;
    }

    [HttpPost]
    [ApiAuthorize(Roles = UserRoles.User)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreateOrUpdateResponse>> Create([FromBody] CreateWorkoutCommand request, CancellationToken cancellationToken = default)
    {
        var result = await _commandDispatcher.DispatchAsync<CreateWorkoutCommand, CreateOrUpdateResponse>(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new {workoutId = result.Id}, result);
    }

    [HttpGet]
    [ApiAuthorize(Roles = UserRoles.User)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<BrowseWorkoutsDto>>> GetWorkoutDates([FromQuery] BrowseWorkoutsQuery query, CancellationToken cancellationToken = default)
    {
        var result = await _queryDispatcher.DispatchAsync<BrowseWorkoutsQuery, IEnumerable<BrowseWorkoutsDto>>(query, cancellationToken);
        return Ok(result);
    }
    
    [HttpGet("{workoutId:Guid}")]
    [ApiAuthorize(Roles = UserRoles.User)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WorkoutDto>> GetById([FromRoute] Guid workoutId, CancellationToken cancellationToken = default)
    {
        var result = await _queryDispatcher.DispatchAsync<GetWorkoutByIdQuery, WorkoutDto>(new GetWorkoutByIdQuery(workoutId), cancellationToken);
        return Ok(result);
    }
    
    [HttpGet("{date:datetime}")]
    [ApiAuthorize(Roles = UserRoles.User)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WorkoutDto>> GetByDate([FromRoute] DateTime date, CancellationToken cancellationToken = default)
    {
        var result = await _queryDispatcher.DispatchAsync<GetWorkoutByDateQuery, WorkoutDto>(new GetWorkoutByDateQuery(date), cancellationToken);
        return Ok(result);
    }
}