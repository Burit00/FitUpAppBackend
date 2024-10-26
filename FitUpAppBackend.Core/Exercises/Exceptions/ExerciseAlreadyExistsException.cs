using FitUpAppBackend.Shared.Abstractions.Exceptions;

namespace FitUpAppBackend.Core.Exercises.Exceptions;

public sealed class ExerciseAlreadyExistsException(string name) : BadRequestFitUpException($"Exercise with name \"{name}\" already exists.")
{
    
}