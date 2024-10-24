namespace FitUpAppBackend.Shared.Abstractions.Exceptions;

public abstract class NotFoundFitUpException(string message) : FitUpException(message, statusCode: 404);