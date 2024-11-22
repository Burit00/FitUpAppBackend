using FitUpAppBackend.Shared.Abstractions.Exceptions;

namespace FitUpAppBackend.Core.Identity.Exceptions;

public sealed class UserNotFoundException() : NotFoundFitUpException("Not found user.")
{
    
}