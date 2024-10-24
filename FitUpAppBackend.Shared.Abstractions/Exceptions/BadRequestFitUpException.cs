namespace FitUpAppBackend.Shared.Abstractions.Exceptions;

public abstract class BadRequestFitUpException(string message) : FitUpException(message, statusCode: 400);