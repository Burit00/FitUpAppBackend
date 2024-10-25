using FitUpAppBackend.Shared.Abstractions.Exceptions;

namespace FitUpAppBackend.Core.ExerciseCategories.Exceptions;

public class ExerciseCategoryAlreadyExistsException(string name) : BadRequestFitUpException($"Exercise category with name: {name} already exists.")
{
    
}