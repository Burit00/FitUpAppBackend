using FitUpAppBackend.Shared.Abstractions.Exceptions;

namespace FitUpAppBackend.Core.Exercises.Exceptions;

public class ExerciseNameIsTooLongException(int maxLength)
    : BadRequestFitUpException($"Exercise name is longer than {maxLength}.");