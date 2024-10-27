using FitUpAppBackend.Shared.Abstractions.Exceptions;

namespace FitUpAppBackend.Core.WorkoutSets.Exceptions;

public class WorkoutSetHasDuplicateParametersException() : BadRequestFitUpException("Duplicate parameters are not allowed.")
{
    
}