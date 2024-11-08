using FitUpAppBackend.Application.Exercises.DTO;
using FitUpAppBackend.Shared.Abstractions.Queries;

namespace FitUpAppBackend.Application.Exercises.Queries.GetExercises;

public sealed record GetExercisesQuery(string? Search = "", IEnumerable<Guid>? ExerciseCategoryIds = null)
    : IQuery<IEnumerable<ExerciseDto>>
{
}