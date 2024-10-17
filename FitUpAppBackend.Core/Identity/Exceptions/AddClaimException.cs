using FitUpAppBackend.Shared.Abstractions.Exceptions;

namespace FitUpAppBackend.Core.Identity.Exceptions;

public sealed class AddClaimException() : FitUpException("Error occured during adding user claims.")
{
    
}