using FitUpAppBackend.Shared.Abstractions.Exceptions;

namespace FitUpAppBackend.Core.Identity.Exceptions;

public sealed class AddClaimException() : BadRequestFitUpException("Error occured during adding user claims.")
{
    
}