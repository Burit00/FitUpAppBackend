using FitUpAppBackend.Application.Workouts.DTO;
using FitUpAppBackend.Shared.Abstractions.Queries;

namespace FitUpAppBackend.Application.Workouts.Queries;

public sealed record BrowseWorkoutsQuery(
    DateTimeOffset? DateStart = null, 
    DateTimeOffset? DateEnd = null, 
    IEnumerable<Guid>? Categories = null) : IQuery<IEnumerable<BrowseWorkoutsDto>>;