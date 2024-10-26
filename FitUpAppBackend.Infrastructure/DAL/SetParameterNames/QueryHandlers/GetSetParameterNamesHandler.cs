using FitUpAppBackend.Application.SetParameterNames.DTO;
using FitUpAppBackend.Application.SetParameterNames.Queries;
using FitUpAppBackend.Infrastructure.DAL.EF.Context;
using FitUpAppBackend.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace FitUpAppBackend.Infrastructure.DAL.SetParameterNames.QueryHandlers;

public sealed class GetSetParameterNamesHandler : IQueryHandler<GetSetParameterNamesQuery, IEnumerable<SetParameterNameDto>>
{
    private readonly EFContext _context;

    public GetSetParameterNamesHandler(EFContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<SetParameterNameDto>> HandleAsync(GetSetParameterNamesQuery query, CancellationToken cancellationToken)
    {
        var result = await _context.SetParameterNames.ToListAsync(cancellationToken);
        return result.Select(spn => new SetParameterNameDto(spn));
    }
}