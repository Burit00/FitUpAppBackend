using FitUpAppBackend.Shared.Abstractions.Exceptions;

namespace FitUpAppBackend.Core.Identity.Exceptions;

public class UserWithEmailAlreadyExistException(string email) : BadRequestFitUpException($"User with email {email} already exists.")
{
    
}