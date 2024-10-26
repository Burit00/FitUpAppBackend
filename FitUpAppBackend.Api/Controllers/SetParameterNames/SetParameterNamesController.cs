using FitUpAppBackend.Application.SetParameterNames.DTO;
using FitUpAppBackend.Application.SetParameterNames.Queries;
using FitUpAppBackend.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace FitUpAppBackend.Api.Controllers.SetParameterNames;

public class SetParameterNamesController : BaseApiController
{
    private readonly IQueryDispatcher _queryDispatcher;

    public SetParameterNamesController(IQueryDispatcher queryDispatcher)
    {
        _queryDispatcher = queryDispatcher;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<SetParameterNameDto>>> GetAll(CancellationToken cancellationToken = default)
    {
        var result =
            await _queryDispatcher.DispatchAsync<GetSetParameterNamesQuery, IEnumerable<SetParameterNameDto>>(
                new GetSetParameterNamesQuery(), cancellationToken);
        return Ok(result);
    }
}