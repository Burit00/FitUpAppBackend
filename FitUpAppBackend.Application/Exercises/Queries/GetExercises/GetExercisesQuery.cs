using FitUpAppBackend.Application.Exercises.DTO;
using FitUpAppBackend.Shared.Abstractions.Queries;

namespace FitUpAppBackend.Application.Exercises.Queries.GetExercises;

public sealed class GetExercisesQuery
    : IQuery<IEnumerable<ExerciseDto>>
{
    public string Search { get; } = "";
    public IEnumerable<Guid> ExerciseCategoryIds { get; } = new List<Guid>();
}