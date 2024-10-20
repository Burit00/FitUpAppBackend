using FitUpAppBackend.Shared.Abstractions.Exceptions;

namespace FitUpAppBackend.Core.Identity.Exceptions;

public class BadUserCredentialsException() : FitUpException("Wrong email or password.")
{
    
}