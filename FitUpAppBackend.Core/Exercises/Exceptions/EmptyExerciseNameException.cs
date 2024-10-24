using FitUpAppBackend.Shared.Abstractions.Exceptions;

namespace FitUpAppBackend.Core.Exercises.Exceptions;

public class EmptyExerciseNameException() : BadRequestFitUpException("Exercise name cannot be empty.");