using FitUpAppBackend.Application.Exercises.DTO;
using FitUpAppBackend.Shared.Abstractions.Queries;

namespace FitUpAppBackend.Application.Exercises.Queries.GetExercise;

public sealed record GetExerciseQuery(Guid Id) : IQuery<ExerciseDetailsDto>;