using FitUpAppBackend.Shared.Abstractions.Exceptions;

namespace FitUpAppBackend.Core.Identity.Exceptions;

public sealed class ResetPasswordRequestException() : BadRequestFitUpException("Not found given email address")
{
    
}