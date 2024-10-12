using FitUpAppBackend.Shared.Abstractions.Exceptions;

namespace FitUpAppBackend.Core.Exercises.Exceptions;

public class EmptyExerciseNameException: FitUpException
{
    public EmptyExerciseNameException() : base("Exercise name cannot be empty.")
    {
    }
}