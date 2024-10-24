using FitUpAppBackend.Shared.Abstractions.Exceptions;

namespace FitUpAppBackend.Core.Identity.Exceptions;

public class BadUserCredentialsException() : BadRequestFitUpException("Wrong email or password.")
{
    
}