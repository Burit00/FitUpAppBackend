namespace FitUpAppBackend.Shared.Abstractions.Exceptions;

public abstract class ForbiddenFitUpException(string message) : FitUpException(message, statusCode: 403);