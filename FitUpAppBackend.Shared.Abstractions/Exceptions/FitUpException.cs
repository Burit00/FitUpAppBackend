namespace FitUpAppBackend.Shared.Abstractions.Exceptions;

public abstract class FitUpException(string message, int statusCode) : Exception(message)
{
    public readonly int StatusCode = statusCode;
}