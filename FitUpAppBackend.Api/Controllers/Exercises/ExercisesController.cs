using FitUpAppBackend.Api.Attributes;
using FitUpAppBackend.Application.Common;
using FitUpAppBackend.Application.Exercises.Commands.CreateExercise;
using FitUpAppBackend.Application.Exercises.Commands.UpdateExercise;
using FitUpAppBackend.Application.Exercises.DTO;
using FitUpAppBackend.Application.Exercises.Queries.GetAnalyticData;
using FitUpAppBackend.Application.Exercises.Queries.GetExercise;
using FitUpAppBackend.Application.Exercises.Queries.GetExercises;
using FitUpAppBackend.Core.Identity.Static;
using FitUpAppBackend.Shared.Abstractions.Commands;
using FitUpAppBackend.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace FitUpAppBackend.Api.Controllers.Exercises;

public class ExercisesController : BaseApiController
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public ExercisesController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }

    [HttpPost]
    [ApiAuthorize(Roles = UserRoles.Admin)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreateOrUpdateResponse>> Create([FromBody] CreateExerciseCommand command,
        CancellationToken cancellationToken = default)
    {
        var result =
            await _commandDispatcher.DispatchAsync<CreateExerciseCommand, CreateOrUpdateResponse>(command,
                cancellationToken);
        return CreatedAtAction(nameof(GetById), new { exerciseId = result.Id }, result.Id);
    }

    [HttpPut("{exerciseId:Guid}")]
    [ApiAuthorize(Roles = UserRoles.Admin)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreateOrUpdateResponse>> Update([FromRoute] UpdateExerciseCommand command,
        CancellationToken cancellationToken = default)
    {
        var result =
            await _commandDispatcher.DispatchAsync<UpdateExerciseCommand, CreateOrUpdateResponse>(command,
                cancellationToken);
        return CreatedAtAction(nameof(GetById), new { exerciseId = result.Id }, result.Id);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ExerciseDto>>> GetAll([FromQuery] GetExercisesQuery query,
        CancellationToken cancellationToken = default)
    {
        var result =
            await _queryDispatcher.DispatchAsync<GetExercisesQuery, IEnumerable<ExerciseDto>>(query,
                cancellationToken);
        return Ok(result);
    }

    [HttpGet("{exerciseId:Guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ExerciseDetailsDto>> GetById([FromRoute] Guid exerciseId,
        CancellationToken cancellationToken = default)
    {
        var result =
            await _queryDispatcher.DispatchAsync<GetExerciseQuery, ExerciseDetailsDto>(new GetExerciseQuery(exerciseId),
                cancellationToken);
        return Ok(result);
    }


    [HttpGet("analytics")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<AnalyticDataArrayDto>>> GetAnalyticData(
        [FromQuery] GetAnalyticDataQuery query,
        CancellationToken cancellationToken = default)
    {
        var result =
            await _queryDispatcher.DispatchAsync<GetAnalyticDataQuery, AnalyticDataArrayDto>(query,
                cancellationToken);
        return Ok(result);
    }
}