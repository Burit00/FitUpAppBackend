using FitUpAppBackend.Api.Attributes;
using FitUpAppBackend.Application.Common;
using FitUpAppBackend.Application.ExerciseCategories.Commands.CreateExerciseCategory;
using FitUpAppBackend.Application.ExerciseCategories.Commands.UpdateExerciseCategory;
using FitUpAppBackend.Application.ExerciseCategories.DTO;
using FitUpAppBackend.Application.ExerciseCategories.Queries.GetExerciseCategories;
using FitUpAppBackend.Application.ExerciseCategories.Queries.GetExerciseCategory;
using FitUpAppBackend.Core.Identity.Static;
using FitUpAppBackend.Shared.Abstractions.Commands;
using FitUpAppBackend.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace FitUpAppBackend.Api.Controllers.ExerciseCategories;

[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public class ExerciseCategoriesController : BaseApiController
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public ExerciseCategoriesController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }

    [HttpPost]
    [ApiAuthorize(Roles = UserRoles.Admin)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateExerciseCategoryCommand command, CancellationToken cancellationToken = default)
    {
        var result =
            await _commandDispatcher.DispatchAsync<CreateExerciseCategoryCommand, CreateOrUpdateResponse>(command, cancellationToken);
       
        return CreatedAtAction(nameof(GetById), new {exerciseCategoryId = result.Id}, result.Id);
    }

    [HttpPut("{exerciseCategoryId:Guid}")]
    [ApiAuthorize(Roles = UserRoles.Admin)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Guid>> Update([FromBody] UpdateExerciseCategoryCommand command, CancellationToken cancellationToken = default)
    {
        var result =
            await _commandDispatcher.DispatchAsync<UpdateExerciseCategoryCommand, CreateOrUpdateResponse>(command, cancellationToken);
       
        return Ok(result.Id);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ExerciseCategoryDto>>> GetAll([FromQuery] GetExerciseCategoriesQuery query, CancellationToken cancellationToken = default)
    {
        var result =
            await _queryDispatcher.DispatchAsync<GetExerciseCategoriesQuery, IEnumerable<ExerciseCategoryDto>>(query, cancellationToken);
        
        return Ok(result);
    }

    [HttpGet("{exerciseCategoryId:Guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ExerciseCategoryDto>> GetById([FromRoute] Guid exerciseCategoryId, CancellationToken cancellationToken = default)
    {
        var result =
            await _queryDispatcher.DispatchAsync<GetExerciseCategoryQuery, ExerciseCategoryDto>(new GetExerciseCategoryQuery(exerciseCategoryId), cancellationToken);
        
        return Ok(result);
    }
    
}