using FitUpAppBackend.Application.WorkoutExercises.DTO;
using FitUpAppBackend.Shared.Abstractions.Queries;

namespace FitUpAppBackend.Application.WorkoutExercises.Queries.GetWorkoutExercise;

public sealed record GetWorkoutExerciseQuery(Guid Id): IQuery<WorkoutExerciseDetailsDto>
{
    
}