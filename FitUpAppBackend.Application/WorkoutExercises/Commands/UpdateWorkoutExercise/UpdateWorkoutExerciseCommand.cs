using System.Text.Json.Serialization;
using FitUpAppBackend.Shared.Abstractions.Commands;

namespace FitUpAppBackend.Application.WorkoutExercises.Commands.UpdateWorkoutExercise;

public sealed record UpdateWorkoutExerciseCommand(Guid WorkoutExerciseOverId)
    : ICommand
{
    [JsonIgnore] 
    public Guid WorkoutExerciseMovedId;
}