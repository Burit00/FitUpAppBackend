using FitUpAppBackend.Application.ExerciseCategories.DTO;
using FitUpAppBackend.Shared.Abstractions.Queries;

namespace FitUpAppBackend.Application.ExerciseCategories.Queries.GetExerciseCategories;

public sealed record GetExerciseCategoriesQuery(string? Search = "") : IQuery<IEnumerable<ExerciseCategoryDto>>;