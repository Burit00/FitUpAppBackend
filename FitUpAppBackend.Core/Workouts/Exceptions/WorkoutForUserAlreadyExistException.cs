namespace FitUpAppBackend.Core.Workouts.Exceptions;

public sealed class WorkoutForUserAlreadyExistException() : FileNotFoundException("Workout for user already exists.")
{
    
}