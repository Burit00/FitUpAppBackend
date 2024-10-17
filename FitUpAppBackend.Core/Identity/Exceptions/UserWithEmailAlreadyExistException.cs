using FitUpAppBackend.Shared.Abstractions.Exceptions;

namespace FitUpAppBackend.Core.Identity.Exceptions;

public class UserWithEmailAlreadyExistException(string email) : FitUpException($"User with email {email} already exists.")
{
    
}