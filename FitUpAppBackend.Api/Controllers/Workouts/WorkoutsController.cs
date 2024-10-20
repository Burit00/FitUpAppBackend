using FitUpAppBackend.Api.Attributes;
using FitUpAppBackend.Application.Workouts.DTO;
using FitUpAppBackend.Application.Workouts.Queries;
using FitUpAppBackend.Core.Identity.Static;
using FitUpAppBackend.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace FitUpAppBackend.Api.Controllers.Workouts;

public class WorkoutsController : BaseApiController
{
    private readonly IQueryDispatcher _queryDispatcher;

    public WorkoutsController(IQueryDispatcher queryDispatcher)
    {
        _queryDispatcher = queryDispatcher;
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
}