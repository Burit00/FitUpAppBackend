using FitUpAppBackend.Shared.Abstractions.Exceptions;

namespace FitUpAppBackend.Core.Identity.Exceptions;

public sealed class EmailVerificationException() : BadRequestFitUpException("Error occurred during email verification.")
{
    
}