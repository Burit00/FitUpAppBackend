using FitUpAppBackend.Application.Workouts.DTO;
using FitUpAppBackend.Shared.Abstractions.Queries;

namespace FitUpAppBackend.Application.Workouts.Queries;

public sealed record BrowseWorkoutsQuery(
    ushort? YearStart = null, 
    ushort? YearEnd = null, 
    IEnumerable<Guid>? Categories = null) : IQuery<IEnumerable<BrowseWorkoutsDto>>;