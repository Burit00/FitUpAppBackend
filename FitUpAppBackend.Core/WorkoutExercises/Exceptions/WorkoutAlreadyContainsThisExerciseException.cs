using FitUpAppBackend.Shared.Abstractions.Exceptions;

namespace FitUpAppBackend.Core.WorkoutExercises.Exceptions;

public class WorkoutAlreadyContainsThisExerciseException() : BadRequestFitUpException("Workout already contains this exercise.")
{
    
}