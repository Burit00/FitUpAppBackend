using FitUpAppBackend.Application.ExerciseCategories.DTO;
using FitUpAppBackend.Shared.Abstractions.Queries;

namespace FitUpAppBackend.Application.ExerciseCategories.Queries.GetExerciseCategory;

public sealed record GetExerciseCategoryQuery(Guid ExerciseCategoryId) : IQuery<ExerciseCategoryDto>;