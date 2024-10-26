using FitUpAppBackend.Shared.Abstractions.Exceptions;

namespace FitUpAppBackend.Core.Exercises.Exceptions;

public sealed class ExerciseNotFoundException(Guid id) : NotFoundFitUpException($"Exercise with id: {id} was not found.")
{
    
}