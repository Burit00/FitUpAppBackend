using FitUpAppBackend.Application.Exercises.DTO;
using FitUpAppBackend.Shared.Abstractions.Queries;

namespace FitUpAppBackend.Application.Exercises.Queries.GetAnalyticData;

public record GetAnalyticDataQuery(Guid ExerciseId, string ParameterName, DateTimeOffset? StartDate, DateTimeOffset? EndDate) : IQuery<AnalyticDataArrayDto>
{
    
}