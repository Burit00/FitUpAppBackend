using FitUpAppBackend.Shared.Abstractions.Exceptions;

namespace FitUpAppBackend.Core.Identity.Exceptions;

public sealed class EmailNotVerifiedException() : BadRequestFitUpException("Your email address is not verified, check your mailbox.")
{
    
}